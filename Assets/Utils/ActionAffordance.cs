using UnityEngine;
using UnityEngine.UI;


public class ActionAffordance : MonoBehaviour 
{
    public static ActionAffordance Instance { get; private set; }

    [SerializeField] Text title;
    [SerializeField] Text action;
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        Hide();
    }

    public void Show(string title, string action, bool isLeft, bool isRight)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        this.title.text = title;
        this.action.text = action;

        this.left.SetActive(isLeft);
        this.right.SetActive(isRight);
    }

    public void Hover()
    {
        transform.GetChild(0).position = Input.mousePosition + (Vector3.up * Screen.height / 10);
    }

    public void Hide()
    {
        if (transform.childCount < 1)
            return;

        if(transform.GetChild(0).gameObject != null)
            transform.GetChild(0).gameObject.SetActive(false);
    }
}
