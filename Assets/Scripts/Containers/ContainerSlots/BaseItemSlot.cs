using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseItemSlot : MonoBehaviour
{
    private ItemSlot slot;

    [SerializeField] Image itemIconImage;
    [SerializeField] TMP_Text itemAmountText;

    public Item CurrentItem => slot.Item;
    public int CurrentAmount => slot.Amount;

    public void Init()
    {
        slot = new ItemSlot();
        slot.OnSlotChanged += SlotChanged;

        slot.Item = null;
    }

    public void AddItemToSlot(Item item, int amount = 1)
    {
        if (slot.Item is null) slot.Item = item;

        slot.Amount += amount;
    }

    private void SlotChanged(ItemSlot slot)
    {
        if (slot.Item is null)
        {
            itemIconImage.enabled = false;
            itemAmountText.text = "";
        }
        else
        {
            itemIconImage.enabled = true;

            itemIconImage.sprite = slot.Item.ItemIcon;
            itemAmountText.text = slot.Amount > 1 ? slot.Amount.ToString() : "";
        }
    }

    public virtual bool CanAddItem(Item item)
    {
        if (slot.Item is null) return false;

        return slot.Item.ID == item.ID;
    }

    public virtual bool IsOverStack(Item item, int amount)
    {

        return slot.Amount + amount > item.MaxStack;
    }

    public virtual bool IsNull()
    {
        return slot.Item is null;
    }

    public virtual bool IsFull()
    {
        return slot.Amount >= slot.Item.MaxStack;
    }
}
