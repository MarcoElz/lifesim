using UnityEngine;
using LifeSim.SceneControl;
using LifeSim.Core.Timer;

namespace LifeSim.Core.Interaction
{
    public class InteractLoadScene : Interactable
    {
        [SerializeField] string sceneName;
        public override void Interact(Vector3 usedPoint)
        {
            base.Interact(usedPoint);

            FindObjectOfType<GameTime>().NextDay();

            GameSceneManager.Instance.LoadSpecificScene("_Scenes/" + sceneName, null);


           
        }
    }
}
