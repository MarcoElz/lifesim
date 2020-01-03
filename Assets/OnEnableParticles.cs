using UnityEngine;

public class OnEnableParticles : MonoBehaviour 
{
    [SerializeField] GameObject particles;
	private void OnEnable()
	{
        GameObject gameObject = Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject, 2f);
	}
}
