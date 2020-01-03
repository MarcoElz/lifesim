using UnityEngine;
using LifeSim.Core.Items;

namespace LifeSim.Core.Interaction
{
    public class InteractCollectableWeight : Interactable
    {
        [SerializeField] ItemDropWeight[] items;

        public override void Interact(Vector3 usedPoint)
        {
            base.Interact(usedPoint);

            Inventory inventory = FindObjectOfType<Inventory>();

            Item item = GetRandomItem();

            if (inventory)
                inventory.Add(item);

            Debug.Log("Player collects: " + item.Name);
            Destroy(this.gameObject);
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
                if (random > count && random < count + items[i].probability)
                {
                    return items[i].item;
                }
                count += items[i].probability;
            }

            return items[0].item;
        }
    }
}


