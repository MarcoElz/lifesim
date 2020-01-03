using UnityEngine;
using LifeSim.Core.Items;
using System.Collections.Generic;

public class ItemDatabase : ScriptableObjectDatabase<Item> 
{
    public override Item GetObjectByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].ID == id)
                return database[i];
        }

        return null;
    }


    public List<Item> GetItems(ItemCategory category = ItemCategory.Unspecified)
    {
        List<Item> items = new List<Item>();

        for (int i = 0; i < database.Count; i++)
        {
            if (category.Equals(ItemCategory.Unspecified) || database[i].Category.Equals(category))
                items.Add(database[i]);
        }

        return items;
    }



}
