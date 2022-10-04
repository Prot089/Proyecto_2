[System.Serializable]
public class InventoryItem 
{
    // This script is for showing the number of objects in the inventory and to avoid showing them one by one
    public InventoryItemModel itemModel;
    public int stackSize;

    public InventoryItem(InventoryItemModel itemModel)
    {
        this.itemModel = itemModel;
        AddStack();
    }

    public void AddStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }
}
