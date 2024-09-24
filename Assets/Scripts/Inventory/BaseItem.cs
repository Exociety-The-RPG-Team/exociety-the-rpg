using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "InventoryItems/BaseItem")]
public class BaseItem : ScriptableObject
{
    [Header("Base Item Fields")]
    public string ItemID;
    public string ItemName;
    public Sprite ItemIcon;    
    public string ItemDescription;

    public int ItemPrice = 99;
    public int MaxStackSize = 99;
}