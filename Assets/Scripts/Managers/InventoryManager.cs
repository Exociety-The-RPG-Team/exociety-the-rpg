using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InventoryManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(InventoryManager).Name);
                    _instance = singletonObject.AddComponent<InventoryManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return _instance;
        }
    }

    private Dictionary<string, int> _inventory; //itemID, amount

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public void LoadInventory()
    {
        try
        {
            string serializedInventory = PlayerPrefs.GetString(GameConstants.PREF_KEY_PLAYER_INVENTORY, "");

            if (!string.IsNullOrEmpty(serializedInventory))
            {
                _inventory = JsonUtility.FromJson<Dictionary<string, int>>(serializedInventory);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error when deserializing inventory: " + e);
        }
    }

    public void SaveInventory()
    {

        try
        {
            string serializedInventory = JsonUtility.ToJson(_inventory);
            PlayerPrefs.SetString(GameConstants.PREF_KEY_PLAYER_INVENTORY, serializedInventory);
        }
        catch(Exception e)
        {
            Debug.LogError("Error when serializing inventory: " + e);
        }
    }

    public bool AddItem(BaseItem item)
    {
        if (_inventory.TryGetValue(item.ItemID, out int amountInInventory))
        {
            if (amountInInventory < item.MaxStackSize)
            {
                _inventory[item.ItemID] = amountInInventory++;
            }
            else
            {
                Debug.Log("Max stack reached, can't add to inventory");
                return false;
            }
        }
        else
        {
            _inventory.Add(item.ItemID, 1);
        }
        
        return true;
    }

    public bool RemoveItem(BaseItem item)
    {
        if (_inventory.TryGetValue(item.ItemID, out int amountInInventory))
        {
            if (amountInInventory > 1)
            {
                _inventory[item.ItemID] = amountInInventory--;
                return true;
            }
            else
            {
                _inventory.Remove(item.ItemID);
                return true;
            }
        }
        else
        {
            Debug.Log("This item is not in the player's inventory.");
            return false;
        }
    }

    public void ClearInventory()
    {
        _inventory.Clear();
        PlayerPrefs.SetString(GameConstants.PREF_KEY_PLAYER_INVENTORY, "");
    }
}

public struct InventoryItem
{
    public BaseItem Item;
    public int Amount;
}