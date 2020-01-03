using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleScreen : MonoBehaviour 
{
    [SerializeField] Button continueButton;


    private void Start()
    {
        DontDestroy[] dontDestroy = FindObjectsOfType<DontDestroy>();
        for (int i = 0; i < dontDestroy.Length; i++)
        {
            Destroy(dontDestroy[i].gameObject);
        }


        int tutorial = PlayerPrefs.GetInt("tutorial", 0);

        continueButton.interactable = tutorial > 0;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Continue()
    {
        SceneManager.LoadScene("House");
    }
}
