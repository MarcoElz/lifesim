using UnityEngine;

public class Follower : MonoBehaviour 
{

    [SerializeField] Vector3 offset;
    Transform target;

	private void Start()
	{
       target = FindObjectOfType<Player>().transform;
	}

    private void Update()
    {
        Vector3 position = target.position;
        position.y = 2.05f;

        position += target.forward * 0.5f;

        position += offset;

        this.transform.position = position;
    }
}
