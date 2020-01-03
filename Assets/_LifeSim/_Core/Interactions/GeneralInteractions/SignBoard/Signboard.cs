using UnityEngine;
using UnityEngine.UI;

public class Signboard : MonoBehaviour 
{
    [SerializeField] Text titleText;
    [SerializeField] Text contentText;

    public void Init(string title, string text)
    {
        //Remove player control
        Player player = player = FindObjectOfType<Player>();
        if (player != null)
            player.IsControllable = false;

        titleText.text = title;
        contentText.text = text;
    }

    public void Close()
    {
        //Return player control
        Player player = player = FindObjectOfType<Player>();
        if (player != null)
            player.IsControllable = true;

        Destroy(this.gameObject);
    }
}
