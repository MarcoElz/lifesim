using UnityEngine;

namespace LifeSim.Core.Items
{
    [CreateAssetMenu(fileName = "Item_Name", menuName = "Inventory/Items", order = 1)]
    public class Item : ScriptableObject
    {
        [SerializeField] int id;
        [SerializeField] protected Sprite sprite;
        [SerializeField] protected string itemName;
        [SerializeField] protected string description;
        [SerializeField] protected ItemCategory category;
        [SerializeField] protected string wiki;
        [SerializeField] protected int price;

        public int ID { get { return id; } private set { } }
        public Sprite Sprite { get { return sprite; } private set { } }
        public string Name { get { return itemName; } private set { } }
        public string Description { get { return description; } private set { } }
        public string Wiki { get { return wiki; } private set { } }
        public int Price { get { return price; } private set { } }
        public ItemCategory Category { get { return category; } private set { } }

        public void Init(int id, Sprite sprite, string name, string description, ItemCategory category, string wiki, int price)
        {
            this.id = id;
            this.sprite = sprite;
            this.itemName = name;
            this.description = description;
            this.category = category;
            this.wiki = wiki;
            this.price = price;
        }

    }

    public enum ItemCategory { Unspecified, Tool, Seed, Resource }
}