using UnityEngine;

namespace LifeSim.Core.Items
{
    public class Crafter : MonoBehaviour
    {
        [SerializeField] string workshopName;
        [SerializeField] Sprite workshopSprite;
        [SerializeField] RecipeRequeriment[] recipes;
        [SerializeField] GameObject craftUIPrefab;
        
        public void InitializeCrafting()
        {
            GameObject craftui =  Instantiate(craftUIPrefab) as GameObject;
            CraftManager craftManager = craftui.GetComponent<CraftManager>();
            craftManager.SetWorkshopData(workshopName, workshopSprite);
            craftManager.SetRecipes(recipes);
        }
    }
    [System.Serializable]
    public struct RecipeRequeriment
    {
        public Recipe recipe;
        public int level;
    }
}