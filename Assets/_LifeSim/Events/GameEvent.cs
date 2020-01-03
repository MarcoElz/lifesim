using UnityEngine;
using System.Collections.Generic;

namespace LifeSim.Events
{
    [CreateAssetMenu(fileName = "Game Event", menuName = "Events/GameEvent", order = 1)]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> listeners = new List<GameEventListener>();

        public void Raise()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
