using UnityEngine;

public class Movement : MonoBehaviour 
{
    Rigidbody rb;
    Player player;

    [SerializeField] float speed = 1.0f;
    
    public bool IsRunning { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
	}

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); 
        float v = Input.GetAxisRaw("Vertical");

        if (!player.IsControllable || (h == 0 && v == 0))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            IsRunning = false;
            return;
        }

        Vector3 direction = new Vector3(h, 0, v);

        rb.velocity = direction * speed;
        rb.angularVelocity = Vector3.zero;
        IsRunning = true;

        transform.LookAt(transform.position + direction);
    }
}
