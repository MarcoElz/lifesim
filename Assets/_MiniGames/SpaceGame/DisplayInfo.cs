using UnityEngine;

public class DisplayInfo : MonoBehaviour 
{
    [SerializeField] string title;
    [SerializeField] [TextArea] string description;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Displayer.Instance.OnStartDisplay(title, description);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Displayer.Instance.OnHideDisplay();
        }
    }
}
