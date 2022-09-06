using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Item Data", menuName = "Inventory System/Create Item", order = 0)]
public class InventoryItemData : ScriptableObject
{
    public InventoryItemModel model;
    public Sprite itemIcon;
}

[System.Serializable]
public class InventoryItemModel
{
    public string id;
    public string itemName;
}
