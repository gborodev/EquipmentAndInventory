using System;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string ID { get; private set; }

    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField][Range(1, 99)] private int stackSize = 1;
    [SerializeField][Range(1, 999999)] private int price = 1;

    public string ItemName => itemName;
    public Sprite ItemIcon => itemSprite;
    public int MaxStack => stackSize;
    public int Price => price;

    private void OnEnable()
    {
        ID = Guid.NewGuid().ToString();
    }

    public virtual Item GetItem()
    {
        return this;
    }

    public abstract string GetItemTitle();
}
