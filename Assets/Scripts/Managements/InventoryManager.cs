using System;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] InventoryContainer inventoryContainer;
    [SerializeField] StashContainer stashContainer;
    [SerializeField] MarketContainer marketContainer;

    [SerializeField] UnityEngine.UI.Image dragPreview;


    [SerializeField] TestItem[] testItems;

    [Serializable]
    public class TestItem
    {
        public Item item;
        public int amount;
    }

    BaseItemSlot dragSlot;

    //Current other container;
    ItemContainer otherContainer;

    private void Start()
    {
        foreach (var item in testItems)
        {
            inventoryContainer.AddItem(item.item, item.amount);
        }

        inventoryContainer.OnSlotPointerEnter += TooltipShow;
        inventoryContainer.OnSlotPointerExit += TooltipHide;
        inventoryContainer.OnSlotBeginDrag += SlotBeginDrag;
        inventoryContainer.OnSlotDrag += SlotDrag;
        inventoryContainer.OnSlotEndDrag += SlotEndDrag;
        inventoryContainer.OnSlotDrop += SlotDrop;

        ChangeContainer(stashContainer);
    }

    public void ChangeContainer(ItemContainer container)
    {
        if (otherContainer != null)
        {
            otherContainer.OnSlotPointerEnter -= TooltipShow;
            otherContainer.OnSlotPointerExit -= TooltipHide;
            otherContainer.OnSlotBeginDrag -= SlotBeginDrag;
            otherContainer.OnSlotDrag -= SlotDrag;
            otherContainer.OnSlotEndDrag -= SlotEndDrag;
            otherContainer.OnSlotDrop -= SlotDrop;
        }
        otherContainer = container;
        otherContainer.OnSlotPointerEnter += TooltipShow;
        otherContainer.OnSlotPointerExit += TooltipHide;
        otherContainer.OnSlotBeginDrag += SlotBeginDrag;
        otherContainer.OnSlotDrag += SlotDrag;
        otherContainer.OnSlotEndDrag += SlotEndDrag;
        otherContainer.OnSlotDrop += SlotDrop;
    }

    private void TooltipShow(BaseItemSlot slot)
    {

    }

    private void TooltipHide(BaseItemSlot slot)
    {

    }

    private void SlotBeginDrag(BaseItemSlot slot)
    {
        if (slot.Item is not null)
        {
            dragSlot = slot;

            dragPreview.gameObject.SetActive(true);
            dragPreview.sprite = dragSlot.Item.ItemIcon;
        }
    }

    private void SlotDrag(BaseItemSlot slot)
    {
        dragPreview.transform.position = Input.mousePosition;
    }

    private void SlotEndDrag(BaseItemSlot slot)
    {
        if (dragSlot != null)
        {
            dragPreview.gameObject.SetActive(false);
            dragSlot = null;
        }
    }

    private void SlotDrop(BaseItemSlot slot)
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
}
