[System.Serializable]
public class InventoryItem 
{
    // Este script es para mostrar numero de objetos en el inventario y evitar que se muestren de uno por uno
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
