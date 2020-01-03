using UnityEngine;
using LifeSim.Core.Items;

namespace LifeSim.Farming
{
    [System.Serializable]
    public class Soil : FarmObject
    {
        [SerializeField] int lives = 3;
        [SerializeField] Crop crop;

        [SerializeField] int daysSincePlant;
        private bool wasWatered;

        private void Start()
        {
            position = transform.position;
            if (crop)
            {
                Instantiate(crop.GetPrefabByDays(daysSincePlant), transform.position, Quaternion.identity);
                if (crop.IsReadyToCollect(daysSincePlant))
                {
                    Instantiate(crop.GetCropPrefab(), transform.position, Quaternion.identity);
                }
            }
        }

        public void Plant(Seed seed)
        {
            if (crop)
                return;

            crop = seed.Crop;
            daysSincePlant = 0;
            Instantiate(crop.GetPrefabByDays(daysSincePlant), transform.position, Quaternion.identity);
        }

        public void Water()
        {
            wasWatered = true;
        }

        public SoilData GetSoilData()
        {
            SoilData soilData = new SoilData();
            soilData.position = new SavVector3(transform.position);
            soilData.lives = lives;
            soilData.days = daysSincePlant;
            soilData.wasWatered = wasWatered;
            if (crop == null)
            {
                soilData.cropid = -1;
            }
            else
            {
                soilData.cropid = crop.ID;
            }
            return soilData;
        }

        public void SetSoilData(int lives, int daysSincePlant, Crop crop, bool wasWatered)
        {
            this.lives = lives;
            this.daysSincePlant = daysSincePlant;
            this.crop = crop;
            this.wasWatered = wasWatered;

            if (wasWatered)
            {
                GetComponent<LifeSim.Core.Interaction.WateredSoilReact>().React(this.transform.position);
            }
        }
    }

    [System.Serializable]
    public class SoilData
    {
        public SavVector3 position;
        public int lives;
        public int days;
        public int cropid;
        public bool wasWatered;
    }
}

