using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewConsumableItem", menuName = "InventoryItems/ConsumableItem")]
public class ConsumableItem : BaseItem
{
    [Header("Consumable Item Fields")]
    public bool CanConsumeOutOfCombat = false;
    public ConsumableTarget Target = ConsumableTarget.ALLIES;
    public List<ConsumableEffect> ConsumableEffects;
}

public enum ConsumableTarget
{
    ALLIES,
    ENEMIES
}

public enum ConsumableStatField
{
    HP,
    SP,
    FOCUS,
    DEF,
    SPEED
}

[Serializable]
public struct ConsumableEffect
{
    public ConsumableStatField Effect;
    public int NumberAffected;
    public int NumberOfTurns;
}