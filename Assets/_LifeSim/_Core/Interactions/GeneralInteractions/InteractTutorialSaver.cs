using UnityEngine;
using LifeSim.SceneControl;
using LifeSim.Core.Items;
using System;
using System.Collections.Generic;
using LifeSim.Farming;

namespace LifeSim.Core.Interaction
{
    public class InteractTutorialSaver : Interactable
    {
        [SerializeField] string sceneName;
        public override void Interact(Vector3 usedPoint)
        {
            base.Interact(usedPoint);

            //Make tutorial made
            int n = PlayerPrefs.GetInt("tutorial", 0);
            n++;
            PlayerPrefs.SetInt("tutorial", n);

            //Save Progress
            SaveData();

            GameSceneManager.Instance.LoadSpecificScene("_Scenes/" + sceneName, null);

        }

        private void SaveData()
        {
            FarmData newGameData = new FarmData();
            //Default values
            newGameData.money = 1000;
            newGameData.farmObjects = new List<SoilData>();
            newGameData.inventoryData = new List<ItemData>();
            newGameData.dateTime = new DateTime(2018, 1, 1);

            Player player = FindObjectOfType<Player>();
            if (player)
            {
                Inventory inventory = player.GetComponent<Inventory>();
                if (inventory)
                {
                    newGameData.inventoryData = inventory.GetInventoryData();
                }
            }

            //Save it
            SaveManager.Save(newGameData);
        }
    }
}

