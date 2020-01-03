using UnityEngine;

public class DestroyAfterTime : MonoBehaviour 
{
    [SerializeField] float time = 5f;

	private void Start()
	{
        Destroy(this.gameObject, time);
	}
}
