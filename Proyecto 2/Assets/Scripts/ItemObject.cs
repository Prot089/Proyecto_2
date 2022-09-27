using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData itemData;

    public void OnHandlePickUp()
    {
        InventorySystem.Instance.Add(itemData);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnHandlePickUp();
        }
    }
}
