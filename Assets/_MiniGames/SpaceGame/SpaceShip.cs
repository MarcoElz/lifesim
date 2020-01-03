using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceShip : MonoBehaviour 
{
    [SerializeField] float speed;
    [SerializeField] float angularSpeed;

    private Rigidbody rb;

	private void Update()
	{
        Move();
	}

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        v = Mathf.Clamp01(v);

        if ((h == 0 && v == 0))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return;
        }

        Vector3 direction = transform.forward * v;
        Vector3 angularDirection = new Vector3(0, h, 0);

        rb.velocity = direction * speed;
        rb.angularVelocity = angularDirection * angularSpeed;
    }
}
