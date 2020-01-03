using UnityEditor;
using UnityEngine;
using LifeSim.Farming;

public class DataGenerator
{
    static int last;
    [MenuItem("Assets/Create/Farming/Crop", false, 1)]
    static void Init()
    {
        Crop crop = ScriptableObject.CreateInstance<Crop>();
        crop.SetIdOnInit(last);
        AssetDatabase.CreateAsset(crop, "Assets/Crop.asset");
        last++;
    }
}
