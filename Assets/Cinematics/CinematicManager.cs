using UnityEngine;
using UnityEngine.Playables;
using LifeSim.Core.Dialogues;

public class CinematicManager : MonoBehaviour 
{
    private PlayableDirector activeDirector;
    private bool isPlaying;

    [SerializeField] PlayableDirector t;

    public static CinematicManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        //StartCinematic(t);
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.O))
        {
            ResumeTimeline();
        }
        */
    }

    public void StartCinematic(PlayableDirector playableDirector)
    {
        activeDirector = playableDirector;
        activeDirector.gameObject.SetActive(true);
    }

    //Called by the TimeMachine Clip (of type Pause)
    public void PauseTimeline(PlayableDirector whichOne)
    {
        activeDirector = whichOne;
        activeDirector.Pause();
        DialogueManager.Instance.SetActionOnEnd(ResumeTimeline);
        //UIManager.Instance.TogglePressSpacebarMessage(true);
    }

    //Called by the InputManager
    public void ResumeTimeline()
    {
        //UIManager.Instance.TogglePressSpacebarMessage(false);
        //UIManager.Instance.ToggleDialoguePanel(false);
        activeDirector.Resume();
        DialogueManager.Instance.RemoveActionOnEnd(ResumeTimeline);
    }

}
