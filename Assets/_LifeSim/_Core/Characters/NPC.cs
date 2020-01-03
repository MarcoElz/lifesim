using UnityEngine;

namespace LifeSim.Core.Characters
{
    [CreateAssetMenu(fileName = "NPC", menuName = "Characters/NPC", order = 1)]
    public class NPC : ScriptableObject
    {
        [SerializeField] string characterName;

        public string Name { get { return characterName; } private set { } }
    }
}
