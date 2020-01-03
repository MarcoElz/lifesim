using UnityEngine;
using LifeSim.Core.Items;
using LifeSim.Core.Interaction;

namespace LifeSim.UI.Items
{
    public class InventoryShortList : MonoBehaviour
    {
        private ItemSlotButton[] shortSlotList;
        private Inventory inventory;

        private void Awake()
        {
            inventory = FindObjectOfType<Inventory>();
            shortSlotList = GetComponentsInChildren<ItemSlotButton>();
            if (shortSlotList != null)
            {
                for (int i = 0; i < shortSlotList.Length; i++)
                {
                    shortSlotList[i].IndexValue = i;
                }
            }
        }

        private void Start()
        {
            DrawShortList();
            shortSlotList[0].Equipped(true);
        }

        public void DrawShortList()
        {
            for (int i = 0; i < shortSlotList.Length; i++)
            {
                Inventory.Slot slot = inventory.Get(i);
                Item item = slot.item;
                Sprite sprite = item != null ? item.Sprite : null;
                shortSlotList[i].Draw(sprite, slot.quantity);
            }
        }

        public void SetEquip(int index)
        {
            FindObjectOfType<Interaction>().SetEquipItem(index);
            for (int i = 0; i < shortSlotList.Length; i++)
            {
                shortSlotList[i].Equipped(false);
            }

            shortSlotList[index].Equipped(true);

        }
    }
}