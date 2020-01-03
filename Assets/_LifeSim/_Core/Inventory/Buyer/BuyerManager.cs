using UnityEngine;
using UnityEngine.UI;
using LifeSim.Core.Items;

public class BuyerManager : MonoBehaviour
{


    [SerializeField] Transform itemList;
    [SerializeField] GameObject itemSlotPrefab;

    [SerializeField] Image availableItemImage;
    [SerializeField] Text availableItemText;
    [SerializeField] GameObject itemActive;
    [SerializeField] GameObject instructionsActive;

    [SerializeField] Text availableItemQuantityText;
    [SerializeField] Text availableItemPriceText;

    [SerializeField] GameObject sell1Button;
    [SerializeField] GameObject sellAllButton;

    [SerializeField] Text marketNameText;
    [SerializeField] Image marketImage;

    private Item actualItem;

    //Cache
    private Player player;
    private Inventory inventory;

    public static BuyerManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnDisable()
    {
        //Clean All
        DestroyChildrenFromParent(itemList);
        marketImage.sprite = null;
        availableItemImage.sprite = null;
        availableItemImage.color = new Color(0f, 0f, 0f, 0f);
        marketNameText.text = "";
        actualItem = null;
        sell1Button.SetActive(false);

        Instance = null;
        Destroy(this.gameObject);

        if (player != null)
            player.IsControllable = true;
    }

    private void OnEnable()
    {
        availableItemImage.color = new Color(0f, 0f, 0f, 0f);
        sell1Button.SetActive(false);
        itemActive.SetActive(false);
        instructionsActive.SetActive(true);

        inventory = FindObjectOfType<Inventory>();
        player = FindObjectOfType<Player>();
        if (player != null)
            player.IsControllable = false;
    }

    public void SetData(string name, Sprite sprite)
    {
        marketNameText.text = name;
        marketImage.sprite = sprite;
    }

    public void SetList(Inventory.Slot[] items)
    {
        //Clean
        DestroyChildrenFromParent(itemList);

        //Create new
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].quantity > 0 && items[i].item.Price > 0)
            {
                GameObject anItem = Instantiate(itemSlotPrefab, itemList) as GameObject;
                anItem.GetComponentInChildren<OnAvailableItemToSellButton>().Init(items[i].item);
            }
            
        }
    }


    //Actions
    public void SetActualItem(Item item)
    {
        actualItem = item;

        int itemQuantity = inventory.GetItemQuantity(item);

        bool canSellOne = itemQuantity > 0;
        bool canSellAll = itemQuantity > 0;

        //Show item
        availableItemText.text = item.Name;
        availableItemQuantityText.text = "Cantidad: " + itemQuantity;
        availableItemPriceText.text = "$" + item.Price;
        availableItemImage.sprite = item.Sprite;

        if (item.Sprite != null)
        {
            availableItemImage.sprite = item.Sprite;
            availableItemImage.color = Color.white;
        }

        //Show Both Sell Button
        itemActive.SetActive(true);
        instructionsActive.SetActive(false);
        sell1Button.SetActive(canSellOne);
        sellAllButton.SetActive(canSellAll);
        //notEnoughMoneyText.SetActive(!canBuy);

    }

    public void SellOneItem()
    {
        if (actualItem == null)
            return;

        Debug.Log(actualItem.Name + " sold!");

        int itemQuantity = inventory.GetItemQuantity(actualItem);

        //Increase Money
        inventory.AddMoney(1 * actualItem.Price);

        //Delete one from inventory
        inventory.ConsumeItem(actualItem, 1);
        itemQuantity--;

        //Refresh
        availableItemQuantityText.text = "Cantidad: " + itemQuantity;
        if(itemQuantity < 1)
            DeselectItem();

        
    }

    public void SellAllItem()
    {
        if (actualItem == null)
            return;

        Debug.Log(actualItem.Name + " sold all!");

        int itemQuantity = inventory.GetItemQuantity(actualItem);

        //Increase Money
        inventory.AddMoney(itemQuantity * actualItem.Price);

        //Delete ALL from inventory
        inventory.ConsumeItem(actualItem, itemQuantity);
        itemQuantity = 0;

        //Refresh   
        availableItemQuantityText.text = "Cantidad: " + itemQuantity;
        if (itemQuantity < 1)
            DeselectItem();

        
    }

    private void DeselectItem()
    {
        actualItem = null;

        sell1Button.SetActive(false);
        sellAllButton.SetActive(false);
        itemActive.SetActive(false);
        instructionsActive.SetActive(true);

        SetList(inventory.GetItems());
    }


    private void DestroyChildrenFromParent(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i) == null)
                continue;

            Destroy(parent.GetChild(i).gameObject);
        }
    }
}
