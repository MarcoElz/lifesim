using UnityEngine;
using LifeSim.Core.Items;

namespace LifeSim.Core.Interaction
{
    public class CollectItemWeightReact : Reactionable
    {
        [SerializeField] ItemDropWeight[] items;

        protected override void Reaction(Vector3 point)
        {
            Inventory inventory = FindObjectOfType<Inventory>();

            Item item = GetRandomItem();

            if (inventory)
                inventory.Add(item);

            Debug.Log("Player collects: " + item.Name);
        }

        private Item GetRandomItem()
        {
            if (items.Length < 1)
                return null;

            int total = 0;
            for (int i = 0; i < items.Length; i++)
            {
                total += items[i].probability;
            }

            int random = Random.Range(0, total);

            int count = 0;
            for (int i = 0; i < items.Length; i++)
            {
                if(random > count && random < count + items[i].probability)
                {
                    return items[i].item;
                }
                count += items[i].probability;
            }

            return items[0].item;
        }
    }

    [System.Serializable]
    public struct ItemDropWeight
    {
        public Item item;
        public int probability;
    }
}

