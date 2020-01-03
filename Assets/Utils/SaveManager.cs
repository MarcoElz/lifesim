using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using LifeSim.Core.Items;
using LifeSim.Farming;
using System;

public class SaveManager : MonoBehaviour 
{

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public Inventory inventory;
    public List<SoilData> fobj;
    public void Save()
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/farmData.dat");
        FarmData data = new FarmData();
        data.farmObjects = new List<SoilData>();
        for(int i = 0; i < FarmObject.farmObjects.Count; i++)
        {

            data.farmObjects.Add(((Soil)FarmObject.farmObjects[i]).GetSoilData());
        }

        data.inventoryData = inventory.GetInventoryData();
        data.money = inventory.GetMoney();

        bf.Serialize(file, data);
        file.Close();
    }

    public static void Save(FarmData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/farmData.dat");

        bf.Serialize(file, data);
        file.Close();
    }

    [SerializeField] GameObject soilPrefab; //TODO REMOVE
    [SerializeField] CropDataBase cropDB;
    public void Load()
    {

        if (File.Exists(Application.persistentDataPath + "/farmData.dat"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/farmData.dat", FileMode.Open);
            FarmData data = (FarmData)bf.Deserialize(file);
            file.Close();
            fobj = data.farmObjects;

            inventory.SetInventoryData(data.inventoryData, data.money);

            //Create them: TODO: REFRACTOR. MOVE AT OTHER PLACE. THIS MUST BE STATIC
            for (int i = 0; i < fobj.Count; i++)
            {
                Vector3 pos = fobj[i].position.ToVector3();
                pos.y = 0f;
                GameObject s =  Instantiate(soilPrefab, pos, Quaternion.identity) as GameObject;
                Crop crop = cropDB.GetObjectByID(fobj[i].cropid);
                if(crop != null)
                    s.GetComponentInChildren<Soil>().SetSoilData(fobj[i].lives, fobj[i].days, cropDB.GetObjectByID(fobj[i].cropid), fobj[i].wasWatered);
            }
            //
        }
    }

    public static FarmData LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/farmData.dat"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/farmData.dat", FileMode.Open);
            FarmData data = (FarmData)bf.Deserialize(file);
            file.Close();
            return data;
        }
        else
        {
            FarmData data = new FarmData();
            //Default values
            data.money = 1000;
            data.farmObjects = new List<SoilData>();
            data.inventoryData = new List<ItemData>();
            //Add default items
            //data.inventoryData.Add();

            data.dateTime = new DateTime(2018, 1, 1);
            return data;
        }
    }
    
}

[System.Serializable]
public class FarmData
{
    public List<SoilData> farmObjects;
    public List<ItemData> inventoryData;
    public int money;
    public DateTime dateTime;
}

[System.Serializable]
public class SavVector3
{
    public float x;
    public float y;
    public float z;

    public SavVector3(Vector3 vector3)
    {
        x = vector3.x;
        y = vector3.y;
        z = vector3.z;
    }

    public Vector3 ToVector3()
    {
        Vector3 vector3 = new Vector3(x, y, z);
        return vector3;
    }
}
