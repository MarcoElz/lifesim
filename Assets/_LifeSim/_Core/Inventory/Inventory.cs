using UnityEngine;
using System.Collections.Generic;
using LifeSim.Events;

namespace LifeSim.Core.Items
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] ItemDatabase itemDatabase;
        [SerializeField] List<Slot> items;

        [SerializeField] short capacity = 30;
        [SerializeField] GameEvent onInventoryUpdate;

        private int money;
        public int GetMoney() { return money; }
        public void AddMoney(int quantity) { money += quantity; }
        public bool ConsumeMoney(int quantity)
        {
            if (money >= quantity)
            {
                money -= quantity;
                return true;
            }
            else
                return false;
        }

        private void Awake()
        {
            items = new List<Slot>(capacity);
        }

        public void Add(Item item)
        {
            if (!IsExistingItem(item))
            {
                if (items.Count < capacity)
                {
                    Slot slot;
                    slot.item = item;
                    slot.quantity = 1;
                    items.Add(slot);
                }
                else
                {   //No space:(
                }
            }
            else
            {
                int index = GetIndex(item);
                Slot slot = Get(index);
                Slot newSlot = new Slot();
                newSlot.item = slot.item;
                newSlot.quantity = slot.quantity + 1;
                items[index] = newSlot;
            }

            onInventoryUpdate.Raise();
        }

        public Slot Get(int index)
        {
            if (items.Count <= index)
                return default(Slot);
            else
                return items[index];
        }

        public int GetIndex(Item item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].item != null && items[i].item.Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void ConsumeItem(Item item, int quantity)
        {
            int index = GetIndex(item);
            if (index > 0)
            {
                Slot slot = Get(index);
                Slot newSlot = new Slot();
                newSlot.item = slot.item;
                newSlot.quantity = slot.quantity - quantity;
                if (newSlot.quantity > 0)
                    items[index] = newSlot;
                else
                    items.RemoveAt(index);
            }

            onInventoryUpdate.Raise();
        }

        public bool IsExistingItem(Item item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].item != null && items[i].item.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsExistingItem(Item item, int amount)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].item != null && items[i].item.Equals(item) && items[i].quantity >= amount)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetItemQuantity(Item item)
        {
            int itemIdex = GetIndex(item);
            Slot slot = Get(itemIdex);

            return slot.quantity;
        }

        public List<ItemData> GetInventoryData()
        {
            List<ItemData> data = new List<ItemData>();
            for (int i = 0; i < items.Count; i++)
            {
                ItemData item = new ItemData();
                if (items[i].item == null)
                    item.id = -1;
                else
                    item.id = items[i].item.ID;
                item.quantity = items[i].quantity;
                item.index = i;
                data.Add(item);

            }
            return data;
        }

        public void SetInventoryData(List<ItemData> itemsData, int money)
        {

            this.money = money;

            if (itemsData == null)
                return;

            for (int i = 0; i < itemsData.Count; i++)
            {
                int index = itemsData[i].index;
                int quant = itemsData[i].quantity;
                int id = itemsData[i].id;
                Item it = itemDatabase.GetObjectByID(id);
                Slot slot = new Slot
                {
                    quantity = quant,
                    item = it
                };
                if (slot.item != null)
                    items.Add(slot);
                /*for (int i = 0; i < itemsData.Count; i++)
                {
                    int index = itemsData[i].index;
                    int quant = itemsData[i].quantity;
                    int id = itemsData[i].id;
                    Item it = itemDatabase.GetObjectByID(id);
                    Slot slot = new Slot
                    {
                        quantity = quant,
                        item = it
                    };
                    items[index] = slot;
                }*/
            }

            onInventoryUpdate.Raise();
        }

        public Slot[] GetItems() { return items.ToArray(); }

        [System.Serializable]
        public struct Slot
        {
            public Item item;
            public int quantity;
        }

        
    }
    [System.Serializable]
    public class ItemData
    {
        public int id;
        public int quantity;
        public int index;
    }
}