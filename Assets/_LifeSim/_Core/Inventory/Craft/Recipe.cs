using UnityEngine;

namespace LifeSim.Core.Items
{
    [CreateAssetMenu(fileName = "Recipe_Name", menuName = "Craft/Recipe", order = 1)]
    public class Recipe : ScriptableObject
    {
        [SerializeField] string recipeName;
        [SerializeField] Ingredient[] ingredients;
        [SerializeField] Item craftItem;

        public string Name { get { return recipeName; } private set { } }
        public Ingredient[] Ingredients { get { return ingredients; } private set { } }
        public Item CraftItem { get { return craftItem; } private set { } }
    }
    [System.Serializable]
    public struct Ingredient
    {
        public Item item;
        public int quantity;
    }
}

