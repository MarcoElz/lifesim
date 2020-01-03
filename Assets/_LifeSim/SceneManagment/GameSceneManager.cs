using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace LifeSim.SceneControl
{
    public class GameSceneManager : MonoBehaviour
    {
        public static GameSceneManager Instance { get; private set; }

        [SerializeField] Animator fader;
        [SerializeField] GameObject loader;
        [SerializeField] int lastSceneIndex = 8;

        private WaitForSeconds fadeAnimationWaitTime;
        private const float ANIMATION_DURATION = 2f;
        private Player player;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }

            Instance = this;
        }

        private void Start()
        {
            fadeAnimationWaitTime = new WaitForSeconds(ANIMATION_DURATION);
            player = GameObject.FindObjectOfType<Player>();
        }

        public void LoadScene(string index)
        {
            LoadSpecificScene(index, null);
        }

        public void LoadSpecificScene(string index, StartPoint startPoint)
        {

            //TODO: REMOVE
            FarmLand land = FindObjectOfType<FarmLand>();
            if (land)
                land.ReSaveLand();


            StartCoroutine(GoToScene(index, startPoint));

            
        }

        private IEnumerator GoToScene(string index, StartPoint startPoint)
        {
            player.IsControllable = false;

            //Fade In
            fader.SetTrigger("In");
            yield return fadeAnimationWaitTime;

            //Load Screen
            loader.SetActive(true);

            //Move Player
            if(startPoint != null)
                player.transform.position = startPoint.transform.position;

            yield return new WaitForEndOfFrame();

            //Load Scene
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
            while (!asyncLoad.isDone)
            {   // Wait until the asynchronous scene fully loads
                yield return null;
            }

            

            //Fade Out
            fader.SetTrigger("Out");
            loader.SetActive(false);

            yield return fadeAnimationWaitTime;
            player.IsControllable = true;
        }

        public void ExitGame()
        {
            Application.Quit();

            if (Application.isEditor)
            {
                Debug.LogWarning("GAME EXIT!");
            }
        }
    }
}