using UnityEngine;
using LifeSim.Core.Items;

namespace LifeSim.Core.Interaction
{
    [RequireComponent(typeof(Crafter))]
    public class InteractCrafter : Interactable
    {

        private Crafter crafter;

        private void Awake()
        {
            crafter = GetComponent<Crafter>();
        }

        public override void Interact(Vector3 usedPoint)
        {
            base.Interact(usedPoint);

            crafter.InitializeCrafting();

        }
    }
}