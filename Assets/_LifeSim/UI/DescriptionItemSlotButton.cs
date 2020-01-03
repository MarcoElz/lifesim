using LifeSim.UI.Items;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionItemSlotButton : MonoBehaviour 
{
    private ItemSlotButton slotButton;
    private Button button;

	private void Awake()
	{
        slotButton = GetComponent<ItemSlotButton>();
        button = GetComponent<Button>();
	}

    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        FindObjectOfType<FullInventory>().DrawInformation(slotButton.IndexValue);
    }
}
