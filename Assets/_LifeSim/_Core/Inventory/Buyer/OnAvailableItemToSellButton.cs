using UnityEngine;
using UnityEngine.UI;
using LifeSim.Core.Items;

public class OnAvailableItemToSellButton : MonoBehaviour
{
    private Item item;

    private Button button;
    [SerializeField] Text nameText;
    [SerializeField] Image itemImage;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickRecipe);
    }

    public void Init(Item item)
    {
        this.item = item;
        nameText.text = item.Name;
        itemImage.sprite = item.Sprite;
    }

    public void OnClickRecipe()
    {
        BuyerManager.Instance.SetActualItem(item);
    }
}
