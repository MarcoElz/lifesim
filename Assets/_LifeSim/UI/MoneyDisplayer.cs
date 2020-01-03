using LifeSim.Core.Items;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplayer : MonoBehaviour 
{
    [SerializeField] Text moneyText;

    public void UpdateMoney()
    {
        int money = FindObjectOfType<Inventory>().GetMoney();
        moneyText.text = "$ " + money + ".00"; 
    }
}
