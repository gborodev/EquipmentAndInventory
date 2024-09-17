using System;

public class ItemSlot
{
    private Item item;
    private int amount;

    public event Action<ItemSlot> OnSlotChanged;

    public Item Item
    {
        get => item;
        set
        {
            item = value;

            OnSlotChanged?.Invoke(this);
        }
    }
    public int Amount
    {
        get => amount;
        set
        {
            amount = value;

            if (amount <= 0) Item = null;

            OnSlotChanged?.Invoke(this);
        }
    }

    public ItemSlot()
    {
        item = null;
        amount = 0;
    }
}
