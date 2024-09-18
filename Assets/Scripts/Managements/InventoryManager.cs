using System;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [Header("Containers")]
    [SerializeField] InventoryContainer inventoryContainer;
    [SerializeField] StashContainer stashContainer;
    [SerializeField] MarketContainer marketContainer;

    [Header("Tooltips")]
    [SerializeField] MarketTooltip marketTooltip;

    [SerializeField] TestItem[] testItems;

    [Serializable]
    public class TestItem
    {
        public Item item;
        public int amount;
    }

    [SerializeField] UnityEngine.UI.Image dragPreview;
    ItemSlot dragSlot;

    private void Start()
    {
        foreach (var item in testItems)
        {
            inventoryContainer.AddItem(item.item, item.amount);
        }

        //Inventory Events
        inventoryContainer.OnSlotPointerEnter += TooltipShow;
        inventoryContainer.OnSlotPointerExit += TooltipHide;
        inventoryContainer.OnSlotBeginDrag += SlotBeginDrag;
        inventoryContainer.OnSlotDrag += SlotDrag;
        inventoryContainer.OnSlotEndDrag += SlotEndDrag;
        inventoryContainer.OnSlotDrop += SlotDrop;
        //Stash Events
        stashContainer.OnSlotPointerEnter += TooltipShow;
        stashContainer.OnSlotPointerExit += TooltipHide;
        stashContainer.OnSlotBeginDrag += SlotBeginDrag;
        stashContainer.OnSlotDrag += SlotDrag;
        stashContainer.OnSlotEndDrag += SlotEndDrag;
        stashContainer.OnSlotDrop += SlotDrop;
        //Market Events
        marketContainer.OnSlotPointerEnter += TooltipShow;
        marketContainer.OnSlotPointerExit += TooltipHide;
        marketContainer.OnSlotBeginDrag += SlotBeginDrag;
        marketContainer.OnSlotDrag += SlotDrag;
        marketContainer.OnSlotEndDrag += SlotEndDrag;
        marketContainer.OnSlotDrop += SellItem;
    }

    private void TooltipShow(ItemSlot slot)
    {

    }

    private void TooltipHide(ItemSlot slot)
    {

    }

    private void SlotBeginDrag(ItemSlot slot)
    {
        if (slot.Item is not null)
        {
            dragSlot = slot;

            dragPreview.gameObject.SetActive(true);
            dragPreview.sprite = dragSlot.Item.ItemIcon;
        }
    }
    private void SlotDrag(ItemSlot slot)
    {
        dragPreview.transform.position = Input.mousePosition;
    }
    private void SlotEndDrag(ItemSlot slot)
    {
        if (dragSlot != null)
        {
            dragPreview.gameObject.SetActive(false);
            dragSlot = null;
        }
    }
    private void SlotDrop(ItemSlot slot)
    {
        if (dragSlot == slot) return;

        if (dragSlot != null)
        {
            if (slot.CanAddStack(dragSlot.Item))
            {
                int remaining = slot.AddItemToSlot(dragSlot.Item, dragSlot.Amount);

                dragSlot.Clear();

                if (remaining > 0)
                {
                    dragSlot.AddItemToSlot(slot.Item, remaining);
                }
            }
            else if (slot.IsNull())
            {
                slot.AddItemToSlot(dragSlot.Item, dragSlot.Amount);
                dragSlot.Clear();
            }
        }

        dragSlot = null;
        dragPreview.gameObject.SetActive(false);
    }

    private void SellItem(ItemSlot slot)
    {
        if (dragSlot.Item != null)
        {
            marketTooltip.InitTooltip(dragSlot, true);
        }
    }

    public void AddItemToInventory(Item item, int amount)
    {
        inventoryContainer.AddItem(item, amount);
    }
}
