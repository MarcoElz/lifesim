using UnityEngine;

public class Shortcuts : MonoBehaviour 
{
    [SerializeField] GameObject pauseUI;
	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseUI.SetActive(!pauseUI.activeSelf);
        }
	}
}
