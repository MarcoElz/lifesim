using UnityEngine;
using LifeSim.Farming;

namespace LifeSim.Core.Items
{
    [CreateAssetMenu(fileName = "Seed_Name", menuName = "Inventory/Item_Seed", order = 3)]
    public class Seed : Tool
    {
        [SerializeField] Crop crop;
        public Crop Crop { get { return crop; } private set { } }

        public void SetCrop(Crop crop)
        {
            this.crop = crop;
        }
    }
}