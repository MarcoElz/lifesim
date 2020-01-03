using UnityEngine;
using UnityEditor;
using LifeSim.Core.Items;
using LifeSim.Farming;
using System.Collections.Generic;

public class ItemDatabaseEditor : EditorWindow
{
    ItemDatabase db;
    Item selectedItem;

    //DB
    private const string DATABASE_FILE_NAME = @"ItemDatabase.asset";
    private const string DATABASE_FOLDER_NAME = @"Database";
    private const string DATABASE_FULL_PATH = @"Assets/" + DATABASE_FOLDER_NAME + "/" + DATABASE_FILE_NAME;

    //Items Instances
    private const string ITEM_FOLDER_NAME = @"Items";
    private const string ITEM_FULL_PATH = @"Assets/" + DATABASE_FOLDER_NAME + "/" + ITEM_FOLDER_NAME;

    //Item Instance Variables
    private string assetName;
    private Sprite sprite;
    private string itemName;
    private string description;
    private ItemCategory category;
    private string wiki;
    private int price;

    //Seed Variables
    private Crop crop;

    //Utils
    private Vector2 itemListScrollPosition;
    private Vector2 itemDetailsScrollPosition;
    private string search;
    private bool isDrawingDetails;
    private bool isCreatingNew;
    private Item detailsItem;


    [MenuItem("Database/ItemEditor")]
    public static void Init()
    {
        ItemDatabaseEditor window = EditorWindow.GetWindow<ItemDatabaseEditor>();
        window.minSize = new Vector2(800, 600);
        window.titleContent = new GUIContent("Item Database");
        window.Show();
        
    }

    private void OnEnable()
    {
        db = AssetDatabase.LoadAssetAtPath(DATABASE_FULL_PATH, typeof(ItemDatabase)) as ItemDatabase;
        if (db == null)
        {
            if (!AssetDatabase.IsValidFolder("Assets/" + DATABASE_FOLDER_NAME))
                AssetDatabase.CreateFolder("Assets", DATABASE_FOLDER_NAME);

            db = ScriptableObject.CreateInstance<ItemDatabase>();
            AssetDatabase.CreateAsset(db, DATABASE_FULL_PATH);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        ResetVariables();
    }

    private void OnGUI()
    {
        ItemEditor();
    }

    void ItemEditor()
    {
        //General
        TopBar();
        GUILayout.BeginHorizontal();
        LeftPanel();
        ItemPanel();
        GUILayout.EndHorizontal();
    }

    void TopBar()
    {
        GUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(true));
        if(GUILayout.Button("Tool")) { category = ItemCategory.Tool; isDrawingDetails = false; }
        if (GUILayout.Button("Seed")) { category = ItemCategory.Seed; isDrawingDetails = false; }
        if (GUILayout.Button("Resource")) { category = ItemCategory.Resource; isDrawingDetails = false; }
        GUILayout.EndHorizontal();
    }
    
    void LeftPanel()
    {
        GUILayout.BeginVertical("Box", GUILayout.ExpandHeight(true), GUILayout.Width(250f));
        if (!category.Equals(ItemCategory.Unspecified))
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(category.ToString(), EditorStyles.boldLabel);
            if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(20))) { isCreatingNew = true; isDrawingDetails = false; }
            GUILayout.EndHorizontal();
            //search = EditorGUILayout.TextField("Search: ", search);
            GUILayout.Space(15f);

            //Scroll of items
        
            List <Item> items = db.GetItems(category);

            itemListScrollPosition = GUILayout.BeginScrollView(itemListScrollPosition, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
            for (int i = 0; i < items.Count; i++)
            {
                //if(search.Equals("") || ...)

                if (items[i].Name.ToUpper().Contains(search.ToUpper()))
                {
                    if (GUILayout.Button(items[i].Name, "Box", GUILayout.ExpandWidth(true)))
                    {
                        isDrawingDetails = true;
                        isCreatingNew = false;
                        detailsItem = items[i];
                    }
                }
            }
            GUILayout.EndScrollView();

        }
        else
        {
            itemListScrollPosition = GUILayout.BeginScrollView(itemListScrollPosition, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
            GUILayout.EndScrollView();
        }
        

        GUILayout.EndVertical();
    }

    
    void ItemPanel()
    {
        if (isDrawingDetails)
        {
            GUILayout.BeginVertical();
            GUILayout.Label("Item Details: " + detailsItem.Name, EditorStyles.boldLabel);
            detailsItem = (Item)EditorGUILayout.ObjectField("Item", detailsItem, typeof(Item));
            GUILayout.EndVertical();

        }
        else if (isCreatingNew)
        {
            GUILayout.BeginVertical();

            if (!category.Equals(ItemCategory.Unspecified))
            {
                EditorStyles.textField.wordWrap = true;

                GUILayout.BeginHorizontal();
                GUILayout.Label("Item Creation: " + category.ToString(), EditorStyles.boldLabel);
                if (GUILayout.Button("Clear")) { ResetVariables(); }
                GUILayout.EndHorizontal();
                GUILayout.Space(15f);

                itemDetailsScrollPosition = GUILayout.BeginScrollView(itemDetailsScrollPosition, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
                GUILayout.BeginHorizontal(GUILayout.Width(50f));
                sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", sprite, typeof(Sprite), allowSceneObjects: false);
                GUILayout.EndHorizontal();
                assetName = EditorGUILayout.TextField("Item asset name", assetName);
                GUILayout.Space(5f);
                itemName = EditorGUILayout.TextField("Item name", itemName);
                GUILayout.Space(5f);
                GUILayout.Label("Description:");
                description = EditorGUILayout.TextArea(description);
                GUILayout.Space(5f);
                GUILayout.Label("Wiki:");
                wiki = EditorGUILayout.TextArea(wiki);
                GUILayout.Space(5f);
                price = EditorGUILayout.IntField("Price", price);
                GUILayout.Space(5f);
                if (category.Equals(ItemCategory.Seed))
                    crop = (Crop)EditorGUILayout.ObjectField("Crop", crop, typeof(Crop));
                GUILayout.EndScrollView();
                GUILayout.Space(5f);
                SaveButton();

            }
            GUILayout.EndVertical();
        }
        else
        {
        }
        
    }


    void SaveButton()
    {
        //Save Button
        if (GUILayout.Button("Save", GUILayout.ExpandWidth(true), GUILayout.Height(30)))
        {
            if (selectedItem != null)
                return;

            string itemFile = assetName + ".asset";

            selectedItem = AssetDatabase.LoadAssetAtPath(ITEM_FULL_PATH + "/" + itemFile, typeof(Item)) as Item;
            if (selectedItem == null)
            {
                //Verify path and folder
                if (!AssetDatabase.IsValidFolder("Assets/" + DATABASE_FOLDER_NAME + "/" + ITEM_FOLDER_NAME))
                    AssetDatabase.CreateFolder("Assets/" + DATABASE_FOLDER_NAME, ITEM_FOLDER_NAME);

                //Create asset
                int id = db.GetLastIndex(); //Get Last id
                if (category.Equals(ItemCategory.Unspecified))
                    return;
                else if (category.Equals(ItemCategory.Tool))
                    selectedItem = ScriptableObject.CreateInstance<LifeSim.Core.Items.Tool>();
                else if (category.Equals(ItemCategory.Seed))
                {
                    selectedItem = ScriptableObject.CreateInstance<Seed>();
                    ((Seed)selectedItem).SetCrop(crop);
                }

                else if (category.Equals(ItemCategory.Resource))
                    selectedItem = ScriptableObject.CreateInstance<Item>();

                selectedItem.Init(id, sprite, itemName, description, category, wiki, price);
                AssetDatabase.CreateAsset(selectedItem, ITEM_FULL_PATH + "/" + itemFile);
                db.Add(selectedItem);
                EditorUtility.SetDirty(db);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                //Restart GUI
                ResetVariables();
                selectedItem = null;
                
            }
            else
            {
                //ALREADY EXISTS
                EditorUtility.DisplayDialog("Warning", "The file " + assetName + " already exists.", "Ok");
                ResetVariables();
                selectedItem = null;
            }
        }
    }

    

    void ResetVariables()
    {
        sprite = null;
        category = ItemCategory.Unspecified;
        assetName = "";
        itemName = "";
        description = "";
        wiki = "";
        price = 0;
        crop = null;

        isDrawingDetails = false;
        isCreatingNew = false;
        detailsItem = null;
}

    GUIStyle GetLabelButtonStyle()
    {
        var s = new GUIStyle();
        var b = s.border;
        b.left = 0;
        b.top = 0;
        b.right = 0;
        b.bottom = 0;
        return s;
    }

}
