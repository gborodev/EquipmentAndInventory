using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] InventoryContainer container;

    [SerializeField] Item testItem;

    private void Start()
    {
        container.AddItem(testItem, 50);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            container.AddItem(testItem, 999);
        }
    }
}
