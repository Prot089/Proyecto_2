using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }

    private Dictionary<InventoryItemData, InventoryItem> _itemDictionary;
    public List<InventoryItem> inventory;

    //Creates a new list
    private void Awake()
    {
        _itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();

        Instance = this;
    }

    void Start()
    {
        var json = PlayerPrefs.GetString("Inventory");
        if (string.IsNullOrEmpty(json)) //Verifies if its null or an empty string
        {
            inventory = new List<InventoryItem>(); //Its null
        }
        else //Empty string
        {
            inventory = JsonConvert.DeserializeObject<List<InventoryItem>>(json);
            Debug.Log(json);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var json = JsonConvert.SerializeObject(inventory);
            PlayerPrefs.SetString("Inventory",json);
            Debug.Log(json);
        }
    }

    //Avoids creating a new list every time you pick an item up so you can accumulate them
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

        Save();
    }

    //Function that removes the object from the list and the list if reaches 0
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

    void Save()
    {
        var json = JsonConvert.SerializeObject(inventory);
        PlayerPrefs.SetString("Inventory", json);
        Debug.Log(json);
    }
}
