using UnityEngine;
namespace LifeSim.Core.Interaction
{
    public class InteractSign : Interactable
    {
        [SerializeField] GameObject signUIPrefab;

        [SerializeField] string title;
        [SerializeField] [TextArea] string text;

        public override void Interact(Vector3 usedPoint)
        {
            base.Interact(usedPoint);

            GameObject signui = Instantiate(signUIPrefab) as GameObject;
            Signboard signboard = signui.GetComponent<Signboard>();
            signboard.Init(title, text);
        }
    }
}
