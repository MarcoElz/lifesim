using UnityEngine;
using LifeSim.Core.Characters;
using LifeSim.Core.Dialogues;

namespace LifeSim.Core.Interaction
{
    public class InteractDialogue : Interactable
    {
        [SerializeField] Dialogue basicDialogue;
        [SerializeField] NPC npc;

        public override void Interact(Vector3 usedPoint)
        {
            base.Interact(usedPoint);

            Debug.Log("Player taks with: " + npc.Name);
            DialogueManager.Instance.StartDialogue(npc, basicDialogue);
        }

        public void ChangeDialogue(Dialogue newDialogue)
        {
            if (newDialogue != null)
                basicDialogue = newDialogue;
        }
    }
}
