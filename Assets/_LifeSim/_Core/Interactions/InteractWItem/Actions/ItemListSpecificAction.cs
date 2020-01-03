using UnityEngine;
using LifeSim.Core.Items;

namespace LifeSim.Core.Interaction
{
    public class ItemListSpecificAction : Actionable
    {
        [SerializeField] Item[] usableItems;

        public override void Execute(Item usedItem, Vector3 usedPoint)
        {
            if (IsValidItem(usableItems, usedItem))
            {
                Action(usedPoint);
            }
            else
            {
                //Wrong Item
            }
        }

        protected override void Action(Vector3 point)
        {
            base.Action(point);
        }


        public bool IsValidItem(Item[] items, Item usedItem)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Equals(usedItem))
                    return true;
            }
            return false;
        }
    }
}
