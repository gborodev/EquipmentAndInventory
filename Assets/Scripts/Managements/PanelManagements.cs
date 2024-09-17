using UnityEngine;
using UnityEngine.UI;

public class PanelManagements : Singleton<PanelManagements>
{
    [Header("Panels")]
    [SerializeField] private UIPanel equipmentPanel;
    [SerializeField] private UIPanel statsPanel;
    [SerializeField] private UIPanel inventoryPanel;
    [SerializeField] private UIPanel stashPanel;
    [SerializeField] private UIPanel marketPanel;

    [Header("Buttons")]
    [SerializeField] private Button equipmentButton;
    [SerializeField] private Button stashButton;
    [SerializeField] private Button marketButton;

    private void Start()
    {
        equipmentButton.onClick.AddListener(EquipmentButtonHandler);
        stashButton.onClick.AddListener(StashButtonHandler);
        marketButton.onClick.AddListener(MarketButtonHandler);

        marketPanel.Hide();
    }

    private void EquipmentButtonHandler()
    {
        if (equipmentPanel.IsActive) equipmentPanel.Hide();
        else equipmentPanel.Show();
    }

    private void StashButtonHandler()
    {
        stashPanel.Show();
        marketPanel.Hide();
    }

    private void MarketButtonHandler()
    {
        marketPanel.Show();
        stashPanel.Hide();
    }
}
