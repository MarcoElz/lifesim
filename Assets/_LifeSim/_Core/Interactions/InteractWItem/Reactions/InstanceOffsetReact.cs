using UnityEngine;

namespace LifeSim.Core.Interaction
{
    public class InstanceOffsetReact : Reactionable
    {
        [SerializeField] GameObject producedPrefab;
        [SerializeField] Vector3 offset;

        protected override void Reaction(Vector3 point)
        {
            Vector3 pos = point + offset;

            Instantiate(producedPrefab, pos, Quaternion.identity);
        }
    }
}