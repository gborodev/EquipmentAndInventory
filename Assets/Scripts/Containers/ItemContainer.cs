using System;
using UnityEngine;

public class ItemContainer : MonoBehaviour, IItemContainer
{
    [SerializeField] ItemSlot[] containerSlots;

    public event Action<ItemSlot> OnSlotPointerEnter;
    public event Action<ItemSlot> OnSlotPointerExit;
    public event Action<ItemSlot> OnSlotBeginDrag;
    public event Action<ItemSlot> OnSlotDrag;
    public event Action<ItemSlot> OnSlotEndDrag;
    public event Action<ItemSlot> OnSlotDrop;

    public event Action<Item, int> OnRemainingItem;

    protected virtual void Awake()
    {
        containerSlots = GetComponentsInChildren<ItemSlot>(false);

        foreach (var slot in containerSlots)
        {
            slot.OnEnterEvent += (slot) => EventHandler(slot, OnSlotPointerEnter);
            slot.OnExitEvent += (slot) => EventHandler(slot, OnSlotPointerExit);
            slot.OnBeginDragEvent += (slot) => EventHandler(slot, OnSlotBeginDrag);
            slot.OnDragEvent += (slot) => EventHandler(slot, OnSlotDrag);
            slot.OnEndDragEvent += (slot) => EventHandler(slot, OnSlotEndDrag);
            slot.OnDropEvent += (slot) => EventHandler(slot, OnSlotDrop);
        }
    }

    private void EventHandler(BaseItemSlot slot, Action<ItemSlot> action)
    {
        action?.Invoke((ItemSlot)slot);
    }

    public virtual bool AddItem(Item item, int amount = 1)
    {
        int remaining = 0;

        for (int i = 0; i < containerSlots.Length; i++)
        {
            if (containerSlots[i].CanAddStack(item) && !containerSlots[i].IsFull())
            {
                remaining = containerSlots[i].AddItemToSlot(item, amount);

                if (remaining > 0)
                {
                    if (AddItem(item, remaining))
                    {
                        return true;
                    }
                }

                return true;
            }
        }

        for (int i = 0; i < containerSlots.Length; i++)
        {
            if (containerSlots[i].IsNull())
            {
                remaining = containerSlots[i].AddItemToSlot(item, amount);

                if (remaining > 0)
                {
                    if (AddItem(item, remaining))
                    {
                        return true;
                    }
                }

                return true;
            }
        }

        if (remaining > 0)
        {
            OnRemainingItem?.Invoke(item, remaining);
        }

        return false;
    }

    public virtual bool RemoveItem(Item item, int amount)
    {
        return true;
    }

    public virtual int ItemCount(Item item)
    {
        return -1;
    }
}
