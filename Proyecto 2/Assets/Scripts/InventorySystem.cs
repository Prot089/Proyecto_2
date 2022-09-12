using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    private Dictionary<InventoryItemData, InventoryItem> _itemDictionary;
    public List<InventoryItem> inventory;

    //Crea una nueva lista con el nuevo item
    private void Awake()
    {
        inventory = new List<InventoryItem>();
        _itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();

        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var json = JsonConvert.SerializeObject(inventory);
            Debug.Log(json);
        }
    }

    //Evita que cada que tomes un nuevo item crees una nueva lista, por lo que permite acumularlos
    public void Add(InventoryItemData itemData)
    {
        if(_itemDictionary.TryGetValue(itemData, out InventoryItem value))
        {
            Debug.Log("Sumar stack en item.");
            value.AddStack();
        }
        else
        {
            Debug.Log("Agregar un nuevo item.");
            InventoryItem newItem = new InventoryItem(itemData.model);
            inventory.Add(newItem);
            _itemDictionary.Add(itemData, newItem);
        }
    }

    //Función que remueve el objeto y la lista si es que llega a 0
    public void Remove(InventoryItemData itemData)
    {
        if(_itemDictionary.TryGetValue(itemData, out InventoryItem value))
        {
            value.RemoveFromStack();

            if(value.stackSize == 0)
            {
                inventory.Remove(value);
                _itemDictionary.Remove(itemData);
            }
        }
    }
}
