using UnityEngine;
using LifeSim.Core.Items;

public class Seller : MonoBehaviour 
{

    [SerializeField] string sellerName;
    [SerializeField] Sprite sprite;
    [SerializeField] Item[] onSellItems;
    [SerializeField] GameObject sellerUIPrefab;

    public void Init()
    {
        GameObject sellerui = Instantiate(sellerUIPrefab) as GameObject;
        SellerManager sellerManager = sellerui.GetComponent<SellerManager>();
        sellerManager.SetData(sellerName, sprite);
        sellerManager.SetList(onSellItems);
    }
}
