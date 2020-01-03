using UnityEngine;
using LifeSim.Core.Timer;
using LifeSim.SceneControl;

namespace LifeSim.Core.Interaction
{
    public class InteractSleep : Interactable
    {
        public override void Interact(Vector3 usedPoint)
        {
            base.Interact(usedPoint);

            GameTime.Instance.NextDay();
            GameManager.Instance.SaveData();
            GameSceneManager.Instance.LoadSpecificScene("_Scenes/" + "House", null);

        }
    }
}