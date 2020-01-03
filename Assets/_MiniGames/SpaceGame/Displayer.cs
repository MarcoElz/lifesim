using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Displayer : MonoBehaviour 
{
    [SerializeField] Text titleText;
    [SerializeField] Text descriptionText;
    [SerializeField] float speed;

    public static Displayer Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void OnStartDisplay(string title, string description)
    {
        StartCoroutine(DisplayBothText(title, description));
        
       
    }

    public void OnHideDisplay()
    {
        StopAllCoroutines();
        titleText.text = "";
        descriptionText.text = "";
    }

    IEnumerator DisplayBothText(string title, string description)
    {
        yield return StartCoroutine(DisplayTextRoutine(titleText, title));
        yield return StartCoroutine(DisplayTextRoutine(descriptionText, description));
    }

    IEnumerator DisplayTextRoutine(Text text, string str)
    {
        string actualText = "";
        for(int i = 0; i < str.Length; i++)
        {
            actualText += str[i];
            text.text = actualText;
            yield return new WaitForSeconds(speed);
        }
    }
}
