using UnityEngine;
using UnityEngine.UI;
using LifeSim.Core.Items;

public class RecipeButton : MonoBehaviour 
{
    private Recipe recipe;
    private int requiredLevel;

    private Button button;
    [SerializeField] Text recipeNameText;
    [SerializeField] Image recipeItemImage;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickRecipe);
    }

    public void Init(Recipe recipe, int level, bool isInteractable)
    {
        this.recipe = recipe;
        this.requiredLevel = level;
        recipeNameText.text = isInteractable ? recipe.Name : "????????????????????????";
        if (isInteractable)
            recipeItemImage.sprite = recipe.CraftItem.Sprite;

        button.interactable = isInteractable;
    }

    public void OnClickRecipe()
    {
        CraftManager.Instance.SetRecipeIngredients(recipe);
    }
}
