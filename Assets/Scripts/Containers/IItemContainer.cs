public interface IItemContainer
{
    public bool AddItem(Item item, int amount = 1);
    public bool RemoveItem(Item item);
    public int ItemCount(Item item);
}
