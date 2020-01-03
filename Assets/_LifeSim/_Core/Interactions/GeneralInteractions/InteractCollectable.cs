using UnityEngine;
using LifeSim.Core.Items;

namespace LifeSim.Core.Interaction
{
    public class InteractCollectable : Interactable
    {
        [SerializeField] Item item;

        public override void Interact(Vector3 usedPoint)
        {
            base.Interact(usedPoint);

            Inventory inventory = FindObjectOfType<Inventory>();

            if (inventory)
                inventory.Add(item);

            Debug.Log("Player collects: " + item.Name);
            Destroy(this.gameObject);
        }
    }
}