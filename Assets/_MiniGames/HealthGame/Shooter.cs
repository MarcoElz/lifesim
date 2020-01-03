using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour 
{
    [SerializeField] Transform shootPosition;
    [SerializeField] GameObject bullet;
    [SerializeField] float waitTime;
    [SerializeField] float waitTimeToShoot;

    Player player;
    bool canShoot;

    private void Awake()
    {
        player = GetComponent<Player>();
        canShoot = true;
    }

    private void Update()
    {
        if (!canShoot)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        player.IsControllable = false;
        canShoot = false;

        RaycastHit hit = RayCastFromCamera();

        Vector3 look = hit.point;
        look.y = transform.position.y;
        transform.LookAt(look);

        GameObject b = Instantiate(bullet, shootPosition.position, shootPosition.rotation) as GameObject;
        Destroy(b, 5f);

        yield return new WaitForSeconds(waitTimeToShoot);
        canShoot = true;

        yield return new WaitForSeconds(waitTime - waitTimeToShoot);
        FindObjectOfType<Player>().IsControllable = true;
    }



    private RaycastHit RayCastFromCamera()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            return hit;
        }

        return hit;
    }
}
