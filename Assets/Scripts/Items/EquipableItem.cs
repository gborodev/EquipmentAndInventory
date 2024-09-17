using UnityEngine;

public enum EquipmentType { None, Weapon, Armor, Helmet, Boots, Ring, Amulet }

[CreateAssetMenu(menuName = "Item/Equipable Item")]
public class EquipableItem : Item
{
    [Header("Equipment Variables")]
    [SerializeField] private EquipmentType equipmentType;

    public override string GetItemTitle()
    {
        return equipmentType.ToString();
    }
}
