using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewKeyItem", menuName = "InventoryItems/KeyItem")]
public class KeyItem : BaseItem
{
    [Header("Equipment Item Fields")]
    public string KeyItemID;
}