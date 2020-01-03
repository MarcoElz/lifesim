using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using LifeSim.Core.Dialogues;
using LifeSim.Core.Characters;

[Serializable]
public class DialogueBehaviour : PlayableBehaviour
{
    [SerializeField] NPC npc;
    [SerializeField] Dialogue dialogue; 

    public bool hasToPause = true;

    private bool clipPlayed = false;
    private bool pauseScheduled = false;
    private PlayableDirector director;

    public override void OnPlayableCreate(Playable playable)
    {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!clipPlayed
            && info.weight > 0f)
        {
            DialogueManager.Instance.StartDialogue(npc, dialogue);

            if (Application.isPlaying)
            {
                if (hasToPause)
                {
                    pauseScheduled = true;
                }
            }

            clipPlayed = true;
        }
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (pauseScheduled)
        {
            pauseScheduled = false;
            CinematicManager.Instance.PauseTimeline(director);
        }
        else
        {
            //UIManager.Instance.ToggleDialoguePanel(false);
        }

        clipPlayed = false;
    }

}