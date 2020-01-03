using UnityEngine;

public class Bullet : MonoBehaviour 
{
    [SerializeField] float speed;

    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
	{
        
        rb.velocity = transform.up * speed;
    }

    private void Update()
    {
        
    }
}
