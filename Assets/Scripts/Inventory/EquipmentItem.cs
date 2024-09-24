using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEquipmentItem", menuName = "InventoryItems/EquipmentItem")]
public class EquipmentItem : BaseItem
{
    [Header("Equipment Item Fields")]
    public List<EquipmentEffect> EquipmentEffect;
}

public enum EquipmentStatField
{
    CONSTITUTION,
    SPEED,
    ATTACK,
    DEFENCE
}

[Serializable]
public struct EquipmentEffect
{
    public EquipmentStatField StatEffect;
    public int EffectAmount;
}