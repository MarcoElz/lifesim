using UnityEngine;

namespace LifeSim.SceneControl
{
    [ExecuteInEditMode]
    public class StartPoint : MonoBehaviour
    {
        SceneStartPoint start;

        private void Update()
        {
            if (start == null)
                return;

            if (Application.isEditor && !Application.isPlaying)
            {
                start.startPosition = this.transform.position;
            }
        }
    }
}
