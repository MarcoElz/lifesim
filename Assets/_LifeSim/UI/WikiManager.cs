using UnityEngine;
using UnityEngine.UI;

public class WikiManager : MonoBehaviour 
{
    [SerializeField] Text titleText;
    [SerializeField] Text descriptionText;
    
    public static WikiManager Instance { get; private set; }
	private void Awake()
	{
        if (Instance == null)
            Instance = this;
	}

    public void ChangeInfo(string title, string description)
    {
        titleText.text = title;
        descriptionText.text = description;
    }
}
