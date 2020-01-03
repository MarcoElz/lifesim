using UnityEngine;

namespace LifeSim.Farming
{
    //[CreateAssetMenu(fileName = "Crop_Name", menuName = "Farming/Crop", order = 1)]
    [System.Serializable]
    public class Crop : ScriptableObject
    {
        [SerializeField] int id;
        [SerializeField] CropStage[] stages;
        [SerializeField] GameObject crop;
        [SerializeField] int daysToCollect;

        public int ID { get { return id; } private set { } }

        public void Init(int id, GameObject crop, CropStage[] stages, int daysToCollect)
        {
            this.id = id;
            this.crop = crop;
            this.stages = stages;
            this.daysToCollect = daysToCollect;

        }

        public GameObject GetPrefabByDays(int days)
        {
            GameObject prefab = stages[0].prefab;

            for (int i = 0; i < stages.Length - 1; i++)
            {
                if (days > stages[i].time)
                {
                    prefab = stages[i+1].prefab;
                }
            }

            return prefab;
        }

        public bool IsReadyToCollect(int days)
        {
            if (crop == null)
                return false;

            int lastStageTime = stages[stages.Length - 1].time;
            if (days >= lastStageTime)
            {
                if (days % daysToCollect == 0)
                    return true;
            }
            return false;
        }

        public GameObject GetCropPrefab()
        {
            return crop;
        }

        [System.Serializable]
        public struct CropStage
        {
            public GameObject prefab;
            public int time;
        }

        public void SetIdOnInit(int i)
        {
            if (Application.isPlaying)
                return;
        }

        
    }


}