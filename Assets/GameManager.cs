using LifeSim.Core.Items;
using LifeSim.Core.Timer;
using LifeSim.Farming;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance { get; private set; }

    [SerializeField] GameObject[] initOnStartup;

    FarmData farmData;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            InitializeObjects();
        }
    }

    private void InitializeObjects()
    {
        for (int i = 0; i < initOnStartup.Length; i++)
        {
            Instantiate(initOnStartup[i]);
        }

        farmData = SaveManager.LoadData();

        //Load player data
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            Inventory inventory = player.GetComponent<Inventory>();
            if(inventory)
                inventory.SetInventoryData(farmData.inventoryData, farmData.money);
        }

        Debug.Log("Datetime: " + farmData.dateTime);

    }

    public void CropCycle()
    {
        List<SoilData> soil = farmData.farmObjects;

        for (int i = 0; i < soil.Count; i++)
        {
            if (soil[i].cropid == -1) //NO crop
            {
                //Nothing
            }
            else
            {
                soil[i].days++;
                if (soil[i].wasWatered)
                {
                    soil[i].wasWatered = false;
                }
                else
                {
                    soil[i].lives -= 1;
                    if (soil[i].lives < 1)
                    {
                        soil.RemoveAt(i);
                    }
                }
                
            }
        }

        farmData.farmObjects = soil;
    }

    public FarmData GetActualData()
    {
        return farmData;
    }

    public void SetNewFarmObjects(List<SoilData> soilData)
    {
        farmData.farmObjects = soilData;
    }

    public void SaveData()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            Inventory inventory = player.GetComponent<Inventory>();
            if (inventory)
            {
                farmData.inventoryData = inventory.GetInventoryData();
                farmData.money = inventory.GetMoney();
            }
        }

        //Save it
        farmData.dateTime = GameTime.Instance.Today;
        SaveManager.Save(farmData);
    }
}
