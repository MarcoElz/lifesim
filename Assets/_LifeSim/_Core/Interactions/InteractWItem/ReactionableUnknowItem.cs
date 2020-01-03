using UnityEngine;
using LifeSim.Core.Items;
using System.Collections;

namespace LifeSim.Core.Interaction
{
    public abstract class ReactionableUnknowItem : Reactionable
    {
        protected override void Reaction(Vector3 point)
        {
        }

        public void UseItem(Item item)
        {
            StartCoroutine(DelayUse(item));
        }

        private IEnumerator DelayUse(Item item)
        {
            yield return new WaitForSeconds(delay);
            UsingItem(item);
        }

        protected abstract void UsingItem(Item item);
    }
}