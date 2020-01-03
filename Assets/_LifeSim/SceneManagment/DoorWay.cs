using UnityEngine;

namespace LifeSim.SceneControl
{
    public class DoorWay : MonoBehaviour
    {
        private const string PLAYER_TAG = "Player";

        [SerializeField] string sceneName;
        [SerializeField] StartPoint playerPoint;
        [SerializeField] AudioClip nextSong;



        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PLAYER_TAG))
            {
                //Change Scene
                GameSceneManager.Instance.LoadSpecificScene("_Scenes/"+sceneName, playerPoint);

                if (nextSong != null)
                    MusicManager.Instance.ChangeSong(nextSong);
            }
        }
    }
}
