using UnityEngine;
using UnityEngine.Events;
using LifeSim.Core.Characters;

namespace LifeSim.Core.Dialogues
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] UnityEvent onStartDialogue;
        [SerializeField] UnityEvent onEndDialogue;
        [SerializeField] DialogueBox dialogueBox;

        private Dialogue actualDialogue;
        private NPC actualNPC;

        private int dialogueIndex;

        public static DialogueManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

       

        public void StartDialogue(NPC npc, Dialogue dialogue)
        {
            actualDialogue = dialogue;
            actualNPC = npc;
            dialogueIndex = -1;

            onStartDialogue.Invoke();
            dialogueBox.OnStartDialogue(actualNPC.Name);
            NextDialogue();
        }

        private void Update()
        {
            if (Input.anyKeyDown)//Input.GetKeyDown(KeyCode.Space))
            {
                NextDialogue();
            }
        }

        public void NextDialogue()
        {
            dialogueIndex++;
            if (actualDialogue == null)
                return;

            if (dialogueIndex > actualDialogue.Length - 1)
            {
                EndDialogue();
            }
            else
            {
                DialogueComponents components = actualDialogue.GetComponents(dialogueIndex);
                dialogueBox.OnNextDialogue(components.text, null);
            }
        }

        void EndDialogue()
        {
            onEndDialogue.Invoke();
            dialogueBox.OnEndDialogue();
            actualDialogue = null;
            actualNPC = null;
        }

        public void SetActionOnEnd(UnityAction action)
        {
            onEndDialogue.AddListener(action);
        }

        public void RemoveActionOnEnd(UnityAction action)
        {
            onEndDialogue.RemoveListener(action);
        }
    }
}
