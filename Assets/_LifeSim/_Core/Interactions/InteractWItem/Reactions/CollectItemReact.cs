using UnityEngine;
using LifeSim.Core.Items;

namespace LifeSim.Core.Interaction
{
    public class CollectItemReact : Reactionable
    {
        [SerializeField] Item item;

        protected override void Reaction(Vector3 point)
        {
            Inventory inventory = FindObjectOfType<Inventory>();

            if (inventory)
                inventory.Add(item);

            Debug.Log("Player collects: " + item.Name);
        }
    }
}
