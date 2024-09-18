using UnityEngine;
using UnityEngine.UI;

public class PanelManagements : Singleton<PanelManagements>
{
    [Header("Panels")]
    [SerializeField] private ContainerPanel equipmentPanel;
    [SerializeField] private ContainerPanel statsPanel;
    [SerializeField] private ContainerPanel inventoryPanel;
    [SerializeField] private ContainerPanel stashPanel;
    [SerializeField] private ContainerPanel marketPanel;

    [Header("Buttons")]
    [SerializeField] private Button equipmentButton;
    [SerializeField] private Button stashButton;
    [SerializeField] private Button marketButton;

    private ContainerPanel currentPanel;

    private void Start()
    {
        equipmentButton.onClick.AddListener(EquipmentButtonHandler);
        stashButton.onClick.AddListener(StashButtonHandler);
        marketButton.onClick.AddListener(MarketButtonHandler);

        marketPanel.Hide();
        equipmentPanel.Hide();
    }

    private void EquipmentButtonHandler()
    {
        if (equipmentPanel.IsActive) equipmentPanel.Hide();
        else equipmentPanel.Show();
    }

    private void StashButtonHandler()
    {
        if (currentPanel != null)
        {
            if (currentPanel == stashPanel)
            {
                stashPanel.Hide();
                currentPanel = null;
                return;
            }

            currentPanel.Hide();
            currentPanel = null;
        }

        currentPanel = stashPanel;
        currentPanel.Show();
    }

    private void MarketButtonHandler()
    {
        if (currentPanel != null)
        {
            if (currentPanel == marketPanel)
            {
                marketPanel.Hide();
                currentPanel = null;
                return;
            }

            currentPanel.Hide();
            currentPanel = null;
        }

        currentPanel = marketPanel;
        currentPanel.Show();
    }

    private void HideContainers()
    {
        marketPanel.Hide();
        stashPanel.Hide();
    }
}
