using UnityEngine;
using UnityEngine.UI;

public class CraftIngredientSlotUI : MonoBehaviour 
{
    [SerializeField] Image image;
    [SerializeField] Text amount;
    [SerializeField] Outline outline;
    [SerializeField] Color goodColor;
    [SerializeField] Color badColor;

    public void Init(Sprite sprite, int ingredientAmount, bool isEnough)
    {
        image.sprite = sprite;
        amount.text = "x" + ingredientAmount;

        if (isEnough)
        {
            amount.color = Color.black;
            outline.effectColor = goodColor;
        }
        else
        {
            amount.color = Color.red;
            outline.effectColor = Color.red;
        }
    }

}
