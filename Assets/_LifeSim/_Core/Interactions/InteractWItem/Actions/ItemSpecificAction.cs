using UnityEngine;
using LifeSim.Core.Items;

namespace LifeSim.Core.Interaction
{
    public class ItemSpecificAction : Actionable
    {
        [SerializeField] Item usableItem;
        [SerializeField] bool isConsumed;

        public override void Execute(Item usedItem, Vector3 usedPoint)
        {
            if (usedItem.Equals(usableItem))
            {
                if(isConsumed)
                    FindObjectOfType<Inventory>().ConsumeItem(usedItem, 1);
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

    }
}