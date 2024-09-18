using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image itemIconImage;
    [SerializeField] TMP_Text itemAmountText;

    private Item item;
    private int amount;

    public Item Item
    {
        get => item;
        set
        {
            item = value;

            itemIconImage.enabled = item != null;

            if (item != null)
            {
                itemIconImage.sprite = item.ItemIcon;
            }
        }
    }

    public int Amount
    {
        get => amount;
        set
        {
            amount = value;

            itemAmountText.gameObject.SetActive(amount > 1);
            itemAmountText.text = amount.ToString();

            if (Amount <= 0) Item = null;
        }
    }

    //Events
    public event Action<BaseItemSlot> OnEnterEvent;
    public event Action<BaseItemSlot> OnExitEvent;

    private void OnValidate()
    {
        Item = item;
        Amount = amount;
    }

    public int AddItemToSlot(Item item, int amount = 1)
    {
        if (Item is null)
            Item = item;

        if (Amount + amount > Item.MaxStack)
        {
            amount -= Item.MaxStack - Amount;
            Amount = Item.MaxStack;
        }
        else
        {
            Amount += amount;
            amount = 0;
        }

        return amount;
    }

    public void RemoveItemToSlot(Item item, int amount = 1)
    {
        Amount -= amount;
    }

    public virtual bool CanAddStack(Item item)
    {
        if (Item is null) return false;

        return Item.ID == item.ID;
    }

    public virtual bool IsOverStack(Item item, int amount)
    {
        return Amount + amount > item.MaxStack;
    }

    public virtual void Clear()
    {
        Item = null;
        Amount = 0;
    }

    public virtual bool IsNull()
    {
        return Item is null;
    }

    public virtual bool IsFull()
    {
        return Amount >= Item.MaxStack;
    }

    //Event Funcs
    public void OnPointerExit(PointerEventData eventData)
    {
        OnExitEvent?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnterEvent?.Invoke(this);
    }
}
