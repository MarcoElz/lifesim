using UnityEngine;

namespace LifeSim.SceneControl
{
    [CreateAssetMenu(fileName = "Point", menuName = "StartPoint/Point", order = 1)]
    public class SceneStartPoint : ScriptableObject
    {
        public Vector3 startPosition;
    }
}