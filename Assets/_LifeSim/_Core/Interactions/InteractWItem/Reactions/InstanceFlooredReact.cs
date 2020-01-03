using UnityEngine;
using System.Collections;

namespace LifeSim.Core.Interaction
{
    public class InstanceFlooredReact : Reactionable
    {
        [SerializeField] GameObject producedPrefab;

        protected override void Reaction(Vector3 point)
        {
            Vector3 pos = point;
            pos.x = Mathf.Floor(pos.x) + 0.5f;
            pos.y = 0f;
            pos.z = Mathf.Floor(pos.z) + 0.5f;
            Instantiate(producedPrefab, pos, Quaternion.identity);
        }
    }
}