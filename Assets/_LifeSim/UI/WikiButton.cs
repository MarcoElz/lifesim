using UnityEngine;
using UnityEngine.UI;
using LifeSim.Core.Items;

public class WikiButton : MonoBehaviour 
{
    [SerializeField] Item item;
    [SerializeField] Image spriteImage;
    [SerializeField] string overrideName;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        if (item != null)
        {
            spriteImage.sprite = item.Sprite;
            spriteImage.enabled = true;
        }

        if (button != null)
            button.onClick.AddListener(DisplayInfo);

    }

    public void DisplayInfo()
    {
        Debug.Log("Click! on " + gameObject.name);
        overrideName = item == null ? overrideName : item.Name;
        if (item != null)
            WikiManager.Instance.ChangeInfo(overrideName, item.Wiki);
    }
}
