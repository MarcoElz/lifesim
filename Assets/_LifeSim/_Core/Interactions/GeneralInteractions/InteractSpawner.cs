using UnityEngine;

namespace LifeSim.Core.Interaction
{
    public class InteractSpawner : Interactable
    {
        [SerializeField] GameObject spawnPrefab;
        [SerializeField] Vector3 offset;

        public override void Interact(Vector3 usedPoint)
        {
            base.Interact(usedPoint);

            Vector3 pos = transform.position + offset;

            Instantiate(spawnPrefab, pos, Quaternion.identity);
        }
    }
}