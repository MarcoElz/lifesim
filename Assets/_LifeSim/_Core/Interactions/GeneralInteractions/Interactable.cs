using UnityEngine;
namespace LifeSim.Core.Interaction
{
    public class Interactable : MonoBehaviour
    {

        private void Awake()
        {
        }

        public virtual void Interact(Vector3 usedPoint)
        {
            Debug.Log("Interacting with: " + this.gameObject.name);
        }
    }
}