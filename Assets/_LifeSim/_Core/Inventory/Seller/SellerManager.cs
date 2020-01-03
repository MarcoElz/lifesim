using UnityEngine;
using UnityEngine.UI;
using LifeSim.Core.Items;

public class SellerManager : MonoBehaviour 
{
    [SerializeField] Transform itemList;
    [SerializeField] GameObject onSellItemPrefab;

    [SerializeField] Image onSellItemImage;
    [SerializeField] Text onSellItemNameText;
    [SerializeField] GameObject itemActive;
    [SerializeField] GameObject instructionsActive;

    [SerializeField] Text onSellItemDescriptionText;
    [SerializeField] Text onSellItemPriceText;

    [SerializeField] GameObject notEnoughMoneyText;
    [SerializeField] GameObject buyButton;

    [SerializeField] Text marketNameText;
    [SerializeField] Image marketImage;

    private Item actualItem;

    //Cache
    private Player player;
    private Inventory inventory;

    public static SellerManager Instance { get; private set; }
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
        onSellItemImage.sprite = null;
        onSellItemImage.color = new Color(0f, 0f, 0f, 0f);
        marketNameText.text = "";
        actualItem = null;
        buyButton.SetActive(false);

        Instance = null;
        Destroy(this.gameObject);

        if (player != null)
            player.IsControllable = true;
    }

    private void OnEnable()
    {
        onSellItemImage.color = new Color(0f, 0f, 0f, 0f);
        buyButton.SetActive(false);
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

    public void SetList(Item[] items)
    {
        //Clean
        DestroyChildrenFromParent(itemList);

        //Create new
        for (int i = 0; i < items.Length; i++)
        {
            GameObject recipe = Instantiate(onSellItemPrefab, itemList) as GameObject;
            recipe.GetComponentInChildren<OnSellItemButton>().Init(items[i]);
        }
    }


    //Actions
    public void SetActualItem(Item item)
    {
        actualItem = item;

        bool canBuy = actualItem.Price <= inventory.GetMoney();

        //Show item
        onSellItemNameText.text = item.Name;
        onSellItemDescriptionText.text = item.Description;
        onSellItemPriceText.text = "$" + item.Price;
        onSellItemImage.sprite = item.Sprite;

        if (item.Sprite != null)
        {
            onSellItemImage.sprite = item.Sprite;
            onSellItemImage.color = Color.white;
        }

        //Show Buy Button
        itemActive.SetActive(true);
        instructionsActive.SetActive(false);
        buyButton.SetActive(canBuy);
        notEnoughMoneyText.SetActive(!canBuy);

    }

    public void BuySelectedItem()
    {
        if (actualItem == null)
            return;

        //Reduce Money
        bool wasBought = inventory.ConsumeMoney(actualItem.Price);
        if (wasBought)
        {
            inventory.Add(actualItem);
            Debug.Log(actualItem.Name + " bought!");

            if(inventory.GetMoney() < actualItem.Price)
                DeselectItem();
        }



    }

    private void DeselectItem()
    {
        actualItem = null;

        buyButton.SetActive(false);
        itemActive.SetActive(false);
        instructionsActive.SetActive(true);
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
