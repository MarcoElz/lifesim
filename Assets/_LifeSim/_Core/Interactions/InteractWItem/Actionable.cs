using UnityEngine;
using LifeSim.Core.Items;

namespace LifeSim.Core.Interaction
{
    public abstract class Actionable : MonoBehaviour
    {
        public abstract void Execute(Item usedItem, Vector3 usedPoint);

        protected virtual void Action(Vector3 point)
        {
            Reactionable[] reactions = GetComponents<Reactionable>();

            if (reactions != null)
            {
                for (int i = 0; i < reactions.Length; i++)
                {
                    reactions[i].React(point);
                }
            }
        }
    }
}