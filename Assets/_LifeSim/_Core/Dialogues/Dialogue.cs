using UnityEngine;

namespace LifeSim.Core.Dialogues
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogues/Dialogue", order = 1)]
    public class Dialogue : ScriptableObject
    {
        [SerializeField]  DialogueComponents[] dialogues;

        public int Length { get { return dialogues.Length; } private set { } }
        public DialogueComponents GetComponents(int index) { return dialogues[index]; }
    }

    [System.Serializable]
    public struct DialogueComponents
    {
        [TextArea] public string text;
        public Emotion emotion;
    }

    public enum Emotion { Neutral, Happy, Angry, Sad, Surprise }
}
