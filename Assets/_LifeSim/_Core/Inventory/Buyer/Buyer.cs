using UnityEngine;
using LifeSim.Core.Items;

public class Buyer : MonoBehaviour
{

    [SerializeField] string buyerName;
    [SerializeField] Sprite sprite;
    //[SerializeField] Item[] onSellItems;
    [SerializeField] GameObject buyerUIPrefab;

    public void Init()
    {
        GameObject buyerUi = Instantiate(buyerUIPrefab) as GameObject;
        BuyerManager buyerManager = buyerUi.GetComponent<BuyerManager>();
        buyerManager.SetData(buyerName, sprite);
        buyerManager.SetList(FindObjectOfType<Inventory>().GetItems());
    }
}
