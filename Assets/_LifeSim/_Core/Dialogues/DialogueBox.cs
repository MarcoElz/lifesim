using UnityEngine;
using UnityEngine.UI;

namespace LifeSim.Core.Dialogues
{
    public class DialogueBox : MonoBehaviour
    {
        [SerializeField] GameObject dialogueBox;
        [SerializeField] Text npcName;
        [SerializeField] Text dialogueText;
        [SerializeField] Image npcImage;

        public void OnStartDialogue(string npc)
        {
            dialogueBox.SetActive(true);
            npcName.text = npc;
            dialogueText.text = "";
        }

        public void OnNextDialogue(string text, Sprite sprite)
        {
            dialogueText.text = text;
            if (sprite != null)
            {
                npcImage.sprite = sprite;
            }
            else
            {
                npcImage.enabled = false;
            }
        }

        public void OnEndDialogue()
        {
            dialogueBox.SetActive(false);
            npcName.text = "";
            dialogueText.text = "";
            npcImage.enabled = false;
        }
    }
}
