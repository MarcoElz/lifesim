using UnityEngine;
using System.Collections;

public class AncientCube : MonoBehaviour 
{
    [SerializeField] AncientSymbol symbol;
    [SerializeField] float timeToMove;

    bool isMoving;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 forward = FindObjectOfType<Player>().transform.forward;
            if ((forward.x > 0.1f || forward.x < -0.1f) && (forward.z > 0.1f || forward.z < -0.1f))
            {
                forward.x = Mathf.Round(forward.x);
                forward.z = 0;
            }
            GetComponent<Rigidbody>().isKinematic = false;

            if (!isMoving)
            {
                //StartCoroutine(MoveRoutine(forward));
            }


        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 forward = FindObjectOfType<Player>().transform.forward;
            if ((forward.x > 0.1f || forward.x < -0.1f) && (forward.z > 0.1f || forward.z < -0.1f))
            {
                forward.x = Mathf.Round(forward.x);
                forward.z = 0;
            }
            GetComponent<Rigidbody>().isKinematic = true;

            if (!isMoving)
            {
                //StartCoroutine(MoveRoutine(forward));
            }


        }

    }

    //Deprecated
    IEnumerator MoveRoutine(Vector3 direction)
    {
        Debug.Log("enter");
        isMoving = true;
        FindObjectOfType<Player>().IsControllable = false;
        float timer = timeToMove;
        GetComponent<Rigidbody>().isKinematic = false;
        yield return null;
        GetComponent<Rigidbody>().AddForce(direction);
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            //GetComponent<Rigidbody>().AddForce(direction * Time.deltaTime * timeToMove);
            //GetComponent<Rigidbody>().AddForce((direction * Time.deltaTime / timeToMove));
            //transform.position = transform.position + (direction * Time.deltaTime / timeToMove);
            yield return null;
        }
        
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
        //Snap
        Vector3 pos = transform.position;
        pos.x = Mathf.FloorToInt(pos.x) + 0.5f; 
        pos.z = Mathf.Floor(pos.z) + 0.5f;
        transform.position = pos;

        //yield return new WaitForSeconds(0.1f);
        FindObjectOfType<Player>().IsControllable = true;
        isMoving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ButtonTile"))
        {
            AncientButton btn = other.GetComponent<AncientButton>();
            if (symbol.Equals(btn.GetSymbol()))
            {
                btn.Activate();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ButtonTile"))
        {
            AncientButton btn = other.GetComponent<AncientButton>();
            if (symbol.Equals(btn.GetSymbol()))
            {
                btn.DeActivate();
            }
        }
    }
}

public enum AncientSymbol {  Sun, Moon, Star }
