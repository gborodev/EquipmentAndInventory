using UnityEngine;

public class UsableItem : Item
{
    [Header("UsableItem Variables")]
    [SerializeField] private bool isConsumables;

    public override string GetItemTitle()
    {
        return isConsumables ? "Consumables" : "Usable";
    }
}
