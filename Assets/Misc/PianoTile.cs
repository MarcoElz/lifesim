using UnityEngine;

public class PianoTile : MonoBehaviour 
{
    [SerializeField] AudioClip sound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
        }
    }
}
