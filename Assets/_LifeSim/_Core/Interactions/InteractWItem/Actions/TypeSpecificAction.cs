using UnityEngine;
using LifeSim.Core.Items;

namespace LifeSim.Core.Interaction
{
    public class TypeSpecificAction : Actionable
    {
        [SerializeField] ItemCategory requiredCategory;
        [SerializeField] bool isConsumed = true;

        public override void Execute(Item usedItem, Vector3 usedPoint)
        {
            if (usedItem.Category.Equals(requiredCategory))
            {
                ActionUnknownItem(usedItem, usedPoint);
            }
            else
            {
                //Wrong Item
            }
        }

        protected void ActionUnknownItem(Item usedItem, Vector3 point)
        {
            ReactionableUnknowItem[] reactions = GetComponents<ReactionableUnknowItem>();

            if (reactions != null)
            {
                if(isConsumed)
                    FindObjectOfType<Inventory>().ConsumeItem(usedItem, 1);
                for (int i = 0; i < reactions.Length; i++)
                {
                    reactions[i].React(point);
                    reactions[i].UseItem(usedItem);
                }
            }
        }
    }
}
