using UnityEngine;

public class Enemy : MonoBehaviour 
{
    [SerializeField] float speed;
    [SerializeField] GameObject bloodCellParticles;
    [SerializeField] GameObject enemyParticles;
    [SerializeField] GameObject bulletParticles;

    bool isDestroyable;

    Transform cellTarget;
    Rigidbody rb;
    private void Start()
    {
        isDestroyable = true;

        cellTarget = FindCell();
        if (cellTarget == null)
        {
            HealthGameManager.Instance.GameOver();
            return;
        }

        Vector3 target = cellTarget.position;
        target.y = transform.position.y;
        Vector3 dir = -transform.position + target;
        rb = GetComponent<Rigidbody>();
        rb.velocity = dir.normalized * speed;
    }

    private void Update()
    {
        if (!isDestroyable)
            return;

        if (cellTarget == null)
        {
            cellTarget = FindCell();
            rb.velocity = Vector3.zero;
            if(cellTarget == null)
                HealthGameManager.Instance.GameOver();
            return;
        }



        Vector3 target = cellTarget.position;
        target.y = transform.position.y;
        transform.transform.LookAt(target);
        Vector3 dir = -transform.position + target;
        rb.velocity = dir.normalized * speed;
    }

    Transform FindCell()
    {
        BloodCell[] cells = FindObjectsOfType<BloodCell>();
        int index = Random.Range(0, cells.Length);

        if (cells == null || cells.Length < 1)
            return null;

        return cells[index].transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            GameObject bulletP = Instantiate(bulletParticles, other.transform.position, Quaternion.identity) as GameObject;
            Destroy(bulletP, 2f);

            Destroy(other.gameObject);

            if(isDestroyable)
            {
                GameObject enemyP = Instantiate(enemyParticles, transform.position, Quaternion.identity) as GameObject;
                Destroy(enemyP, 2f);
                Destroy(this.gameObject);
            }
        }
        else if(other.CompareTag("BloodCell"))
        {
            Color color = GetComponent<MeshRenderer>().material.color;
            color.r = 1f;
            color.b = 0.3f;
            GetComponent<MeshRenderer>().material.color = color;
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            GameObject p = Instantiate(bloodCellParticles, other.transform.position, Quaternion.identity) as GameObject;
            Destroy(p, 2f);
            Destroy(other.gameObject);
            isDestroyable = false;
            transform.localScale = Vector3.one * 1.1f;
        }
    }

    public void Destroy()
    {
        if (isDestroyable)
        {
            GameObject enemyP = Instantiate(enemyParticles, transform.position, Quaternion.identity) as GameObject;
            Destroy(enemyP, 2f);
            Destroy(this.gameObject);
        }
    }
}
