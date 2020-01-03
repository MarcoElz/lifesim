using UnityEngine;

public class TutorialSave : MonoBehaviour 
{

    private void Start()
    {
        int n = PlayerPrefs.GetInt("tutorial", 0);

        n++;

        PlayerPrefs.SetInt("tutorial", n);
    }
}
