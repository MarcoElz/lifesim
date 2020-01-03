using UnityEngine;
using System.Collections;

namespace LifeSim.Core.Interaction
{
    public abstract class Reactionable : MonoBehaviour
    {
        [SerializeField] protected float delay;

        public void React(Vector3 point)
        {
            StartCoroutine(DelayReact(point));
        }

        private IEnumerator DelayReact(Vector3 point)
        {
            yield return new WaitForSeconds(delay);
            Reaction(point);
        }

        protected abstract void Reaction(Vector3 point);
    }
}