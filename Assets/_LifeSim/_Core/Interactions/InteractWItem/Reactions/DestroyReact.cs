using UnityEngine;

namespace LifeSim.Core.Interaction
{
    public class DestroyReact : Reactionable
    {
        protected override void Reaction(Vector3 point)
        {
            Destroy(this.gameObject);
        }

    }
}