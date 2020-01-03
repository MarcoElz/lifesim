using UnityEngine;
using UnityEngine.UI;

namespace LifeSim.UI.Items
{
    public class ItemSlotButton : MonoBehaviour
    {
        [SerializeField] Image itemImage;
        [SerializeField] Outline equipOutline;
        [SerializeField] Text quantityText;

        [SerializeField] bool clickable;

        private Button button;
        private InventoryShortList shortList;

        public int IndexValue { get; set; }

        private bool isEquipped;

        private void Awake()
        {
            button = GetComponent<Button>();
            shortList = transform.parent.GetComponent<InventoryShortList>();
        }

        private void Start()
        {
            if(clickable)
                button.onClick.AddListener(OnClickSlot);
        }

        public void OnClickSlot()
        {
            shortList.SetEquip(IndexValue);
        }

        public void Draw(Sprite sprite, int quantity)
        {
            if (sprite != null)
            {
                itemImage.sprite = sprite;
                itemImage.enabled = true;
            }
            else
            {
                itemImage.enabled = false;
            }

            quantityText.text = quantity < 2 ? "" : quantity.ToString();

        }

        public void Equipped(bool isEquipped)
        {
            this.isEquipped = isEquipped;
            equipOutline.enabled = this.isEquipped;
        }
    }
}