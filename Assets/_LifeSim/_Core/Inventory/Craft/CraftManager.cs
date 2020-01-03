using UnityEngine;
using UnityEngine.UI;
using LifeSim.Core.Items;

public class CraftManager : MonoBehaviour 
{
    [SerializeField] Transform recipeList;
    [SerializeField] GameObject recipePrefab;

    [SerializeField] Transform ingredientsGrid;
    [SerializeField] GameObject ingredientSlotPrefab;

    [SerializeField] Image craftedItemImage;
    [SerializeField] Text craftedItemNameText;
    [SerializeField] GameObject ingredientsActive;
    [SerializeField] GameObject instructionsActive;

    [SerializeField] GameObject notEnoughItemsText;
    [SerializeField] GameObject createButton;

    [SerializeField] Text workshopNameText;
    [SerializeField] Image workshopImage;

    private Recipe actualRecipe;

    //Cache
    private Player player;
    private Inventory inventory;

    public static CraftManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnDisable()
    {
        //Clean All
        DestroyChildrenFromParent(recipeList);
        DestroyChildrenFromParent(ingredientsGrid);
        workshopImage.sprite = null;
        craftedItemImage.sprite = null;
        craftedItemImage.color = new Color(0f, 0f, 0f, 0f);
        workshopNameText.text = "";
        workshopImage.sprite = null;
        actualRecipe = null;
        createButton.SetActive(false);

        Instance = null;
        Destroy(this.gameObject);

        if(player != null)
            player.IsControllable = true;
    }

    private void OnEnable()
    {
        craftedItemImage.color = new Color(0f, 0f, 0f, 0f);
        createButton.SetActive(false);
        ingredientsActive.SetActive(false);
        instructionsActive.SetActive(true);

        inventory = FindObjectOfType<Inventory>();
        player = FindObjectOfType<Player>();
        if (player != null)
            player.IsControllable = false;
    }

    public void SetWorkshopData(string name, Sprite sprite)
    {
        workshopNameText.text = name;
        workshopImage.sprite = sprite;
    }

    public void SetRecipes(RecipeRequeriment[] recipesRequirement)
    {
        //Clean
        DestroyChildrenFromParent(recipeList);

        //Create new
        for (int i = 0; i < recipesRequirement.Length; i++)
        {
            bool interactable = recipesRequirement[i].level > 0;

            GameObject recipe = Instantiate(recipePrefab, recipeList) as GameObject;
            recipe.GetComponentInChildren<RecipeButton>().Init(recipesRequirement[i].recipe, recipesRequirement[i].level, interactable);

        }
    }

    public void SetRecipeIngredients(Recipe recipe)
    {
        actualRecipe = recipe;

        //Clean
        DestroyChildrenFromParent(ingredientsGrid);
        bool canCraft = true;
        //Create New
        for (int i = 0; i < recipe.Ingredients.Length; i++)
        {
            Ingredient ingredient = recipe.Ingredients[i];

            //Verify if missing ingredient
            bool isEnough = inventory.IsExistingItem(ingredient.item, ingredient.quantity);

            GameObject ingredientUI = Instantiate(ingredientSlotPrefab, ingredientsGrid) as GameObject;
            ingredientUI.GetComponentInChildren<CraftIngredientSlotUI>().Init(ingredient.item.Sprite, ingredient.quantity, isEnough);

            if (!isEnough)
                canCraft = false;
        }

        if(recipe.CraftItem.Sprite != null)
        {
            craftedItemImage.sprite = recipe.CraftItem.Sprite;
            craftedItemImage.color = Color.white;
        }

        craftedItemNameText.text = recipe.Name;


        //Show Create Button
        ingredientsActive.SetActive(true);
        instructionsActive.SetActive(false);
        createButton.SetActive(canCraft);
        notEnoughItemsText.SetActive(!canCraft);
    }

    public void CraftRecipeItem()
    {
        if (actualRecipe == null)
            return;

        for (int i = 0; i < actualRecipe.Ingredients.Length; i++)
        {
            inventory.ConsumeItem(actualRecipe.Ingredients[i].item, actualRecipe.Ingredients[i].quantity);
        }

        inventory.Add(actualRecipe.CraftItem);
        Debug.Log(actualRecipe.CraftItem.Name + " crafted!");
        DeselectRecipe();
    }

    private void DeselectRecipe()
    {
        actualRecipe = null;

        createButton.SetActive(false);
        ingredientsActive.SetActive(false);
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
