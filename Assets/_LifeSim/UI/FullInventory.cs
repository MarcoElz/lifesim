using UnityEngine;
using LifeSim.Core.Items;
using LifeSim.Core.Interaction;
using UnityEngine.UI;

namespace LifeSim.UI.Items
{
    public class FullInventory : MonoBehaviour
    {

        [SerializeField] Text itemName;
        [SerializeField] Text itemDescription;

        private ItemSlotButton[] slotList;
        private Inventory inventory;

        private void Awake()
        {
            inventory = FindObjectOfType<Inventory>();
            slotList = GetComponentsInChildren<ItemSlotButton>();
            if (slotList != null)
            {
                for (int i = 0; i < slotList.Length; i++)
                {
                    slotList[i].IndexValue = i;
                }
            }
        }

        private void OnEnable()
        {
            DrawList();
            //DrawInformation(0);
        }

        public void DrawList()
        {
            for (int i = 0; i < slotList.Length; i++)
            {
                Inventory.Slot slot = inventory.Get(i);
                Item item = slot.item;
                Sprite sprite = item != null ? item.Sprite : null;
                slotList[i].Draw(sprite, slot.quantity);
            }
        }

        public void DrawInformation(int i)
        {
            Item item = inventory.Get(i).item;
            itemName.text = item.Name;
            itemDescription.text = item.Description;
        }
    }
}