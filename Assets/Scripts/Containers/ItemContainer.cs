using UnityEngine;

public class ItemContainer : MonoBehaviour, IItemContainer
{
    [SerializeField] BaseItemSlot[] containerSlots;

    protected virtual void Awake()
    {
        containerSlots = GetComponentsInChildren<BaseItemSlot>(false);

        foreach (var slot in containerSlots)
        {
            slot.Init();
        }
    }

    public virtual bool AddItem(Item item, int amount = 1)
    {
        int remaining = amount;

        for (int i = 0; i < containerSlots.Length; i++)
        {
            if (containerSlots[i].CanAddItem(item) && !containerSlots[i].IsFull())
            {
                if (containerSlots[i].IsOverStack(item, amount))
                {
                    int added = item.MaxStack - containerSlots[i].CurrentAmount;
                    remaining = amount - added;

                    containerSlots[i].AddItemToSlot(item, added);
                    AddItem(item, remaining);

                    return false;
                }
                else
                {
                    containerSlots[i].AddItemToSlot(item, amount);
                    return true;
                }
            }
        }


        for (int i = 0; i < containerSlots.Length; i++)
        {
            if (containerSlots[i].IsNull())
            {
                if (containerSlots[i].IsOverStack(item, amount))
                {
                    int added = item.MaxStack - containerSlots[i].CurrentAmount;
                    remaining = amount - added;

                    containerSlots[i].AddItemToSlot(item, added);
                    AddItem(item, remaining);

                    return false;
                }
                else
                {
                    containerSlots[i].AddItemToSlot(item, amount);
                    return true;
                }
            }
        }


        if (remaining > 0)
        {
            Debug.Log("Item cannot added. Left: " + remaining);
            return false;
        }

        return false;
    }

    public virtual bool RemoveItem(Item item)
    {
        return true;
    }

    public virtual int ItemCount(Item item)
    {
        return -1;
    }
}
