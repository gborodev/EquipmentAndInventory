using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketTooltip : MonoBehaviour
{
    [SerializeField] private Transform panel;

    [SerializeField] private Slider amountSlider;
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private TMP_Text priceText;

    [SerializeField] private Button dealButton;

    ItemSlot currentSlot;
    int amount;

    private void Start()
    {
        amountSlider.onValueChanged.AddListener(SliderChange);
    }

    public void InitTooltip(ItemSlot slot, bool selling)
    {
        currentSlot = slot;

        panel.gameObject.SetActive(true);

        amountSlider.value = 1;
        amountText.text = $"x1 {currentSlot.Item.ItemName}";

        amount = 1;

        dealButton.onClick.RemoveAllListeners();

        if (selling)
        {
            amountSlider.minValue = 1;
            amountSlider.maxValue = currentSlot.Amount;
            dealButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Sell";
            dealButton.onClick.AddListener(SellItem);
        }
        else
        {
            amountSlider.minValue = 1;
            amountSlider.maxValue = 999;
            dealButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Buy";
            dealButton.onClick.AddListener(BuyItem);
        }
    }

    private void SliderChange(float value)
    {
        amount = (int)value;

        amountText.text = $"x{value} {currentSlot.Item.ItemName}";
        priceText.text = $"{currentSlot.Item.Price * value} gold";
    }

    void SellItem()
    {
        GameManager.Instance.Gold += amount * currentSlot.Item.Price;
        currentSlot.Amount -= amount;

        currentSlot = null;
        panel.gameObject.SetActive(false);
    }

    void BuyItem()
    {
        GameManager.Instance.Gold -= amount * currentSlot.Item.Price;
        InventoryManager.Instance.AddItemToInventory(currentSlot.Item, amount);

        currentSlot = null;
        panel.gameObject.SetActive(false);
    }
}
