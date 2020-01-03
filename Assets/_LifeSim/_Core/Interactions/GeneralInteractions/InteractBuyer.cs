using UnityEngine;
using LifeSim.Core.Items;

namespace LifeSim.Core.Interaction
{
    [RequireComponent(typeof(Buyer))]
    public class InteractBuyer : Interactable
    {

        private Buyer buyer;

        private void Awake()
        {
            buyer = GetComponent<Buyer>();
        }

        public override void Interact(Vector3 usedPoint)
        {
            base.Interact(usedPoint);

            buyer.Init();

        }
    }
}
