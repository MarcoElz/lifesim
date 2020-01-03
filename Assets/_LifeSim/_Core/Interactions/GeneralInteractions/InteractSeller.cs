using UnityEngine;
using LifeSim.Core.Items;

namespace LifeSim.Core.Interaction
{
    [RequireComponent(typeof(Seller))]
    public class InteractSeller : Interactable
    {

        private Seller seller;

        private void Awake()
        {
            seller = GetComponent<Seller>();
        }

        public override void Interact(Vector3 usedPoint)
        {
            base.Interact(usedPoint);

            seller.Init();

        }
    }
}
