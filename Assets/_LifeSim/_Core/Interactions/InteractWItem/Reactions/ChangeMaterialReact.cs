using UnityEngine;
namespace LifeSim.Core.Interaction
{
    public class ChangeMaterialReact : Reactionable
    {
        [SerializeField] Material newMaterial;

        protected override void Reaction(Vector3 point)
        {
            GetComponent<MeshRenderer>().sharedMaterial = newMaterial; ;
        }
    }
}