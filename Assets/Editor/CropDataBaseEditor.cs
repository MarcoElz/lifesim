using UnityEngine;
using UnityEditor;
using LifeSim.Farming;
using System.Collections.Generic;

public class CropDataBaseEditor : EditorWindow 
{
    CropDataBase db;
    Crop selectedCrop;

    //DB
    private const string DATABASE_FILE_NAME = @"CropDatabase.asset";
    private const string DATABASE_FOLDER_NAME = @"Database";
    private const string DATABASE_FULL_PATH = @"Assets/" + DATABASE_FOLDER_NAME + "/" + DATABASE_FILE_NAME;

    //Crops Instances
    private const string CROP_FOLDER_NAME = @"Crops";
    private const string CROP_FULL_PATH = @"Assets/" + DATABASE_FOLDER_NAME + "/" + CROP_FOLDER_NAME;

    //Crop Instance Variables
    private string assetName;
    private int daysToCollect;
    private GameObject crop;
    private int stagesNumber;
    private List<GameObject> stages;
    private List<int> stagesDays;
    private Vector2 scrollPosition;


    [MenuItem("Database/CropEditor")]
    public static void Init()
    {
        CropDataBaseEditor window = EditorWindow.GetWindow<CropDataBaseEditor>();
        window.minSize = new Vector2(400, 300);
        window.titleContent = new GUIContent("Crop Database");
        window.Show();
    }

    private void OnEnable()
    {
        db = AssetDatabase.LoadAssetAtPath(DATABASE_FULL_PATH, typeof(CropDataBase)) as CropDataBase;
        if (db == null)
        {
            if (!AssetDatabase.IsValidFolder("Assets/" + DATABASE_FOLDER_NAME))
                AssetDatabase.CreateFolder("Assets", DATABASE_FOLDER_NAME);

            db = ScriptableObject.CreateInstance<CropDataBase>();
            AssetDatabase.CreateAsset(db, DATABASE_FULL_PATH);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        ResetVariables();
    }

    private void OnGUI()
    {
        CropEditorView();
    }


    void CropEditorView()
    {
        //General
        assetName = EditorGUILayout.TextField("Crop Name", assetName);
        daysToCollect = EditorGUILayout.IntField("Days to collect", daysToCollect);
        crop = (GameObject) EditorGUILayout.ObjectField("Crop", crop, typeof(GameObject));

        //Stages
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Stages: " + stagesNumber);
        if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(20)))
        {   //Add new stage
            stagesNumber++;
            stages.Add(null);
            stagesDays.Add(0);
        }
        EditorGUILayout.EndHorizontal();

        //Stages Scroll View
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandHeight(true));
        for (int i = 0; i < stagesNumber; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Stage " + i);
            if (GUILayout.Button("x", GUILayout.Width(20), GUILayout.Height(20)))
            {
                stagesNumber--;
                stages.RemoveAt(i);
                stagesDays.RemoveAt(i);
                if (stagesNumber == 0)
                    break;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel++;
            stages[i] = (GameObject)EditorGUILayout.ObjectField("Game Object: ", stages[i], typeof(GameObject));
            stagesDays[i] = EditorGUILayout.IntField("Time: ", stagesDays[i]);
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndScrollView();


        //Save Button
        if (GUILayout.Button("Save", GUILayout.ExpandWidth(true), GUILayout.Height(30)))
        {
            if (selectedCrop != null)
                return;
            
            string cropFile = assetName + ".asset";

            selectedCrop = AssetDatabase.LoadAssetAtPath(CROP_FULL_PATH + "/" + cropFile, typeof(Crop)) as Crop;
            if (selectedCrop == null)
            {
                //Verify path and folder
                if (!AssetDatabase.IsValidFolder("Assets/" + DATABASE_FOLDER_NAME + "/" + CROP_FOLDER_NAME))
                    AssetDatabase.CreateFolder("Assets/" + DATABASE_FOLDER_NAME, CROP_FOLDER_NAME);

                //Generate CropStage Array from both data list
                Crop.CropStage[] cropStages = new Crop.CropStage[stagesNumber];
                for (int i = 0; i < stagesNumber; i++)
                {
                    cropStages[i].prefab = stages[i];
                    cropStages[i].time = stagesDays[i];
                }

                int id = db.GetLastIndex(); //Get Last id

                //Create asset and save
                selectedCrop = ScriptableObject.CreateInstance<Crop>();
                selectedCrop.Init(id,crop, cropStages, daysToCollect);
                AssetDatabase.CreateAsset(selectedCrop, CROP_FULL_PATH + "/" + cropFile);
                db.Add(selectedCrop);
                EditorUtility.SetDirty(db);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                //Restart GUI
                ResetVariables();
                selectedCrop = null;
            }
            else
            {
                //ALREADY EXISTS
                EditorUtility.DisplayDialog("Warning", "The file " + assetName + " already exists.", "Ok");
                ResetVariables();
                selectedCrop = null;
            }
 
        }
    }

    void ResetVariables()
    {
        stagesNumber = 0;
        stages = new List<GameObject>();
        stagesDays = new List<int>();

        assetName = "";
        daysToCollect = 0;
        crop = null;
    }

}
