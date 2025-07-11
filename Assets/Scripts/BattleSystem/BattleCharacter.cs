using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    [Header("Character Info (ScriptableObject)")]
    public CharacterInfo characterInfo;

    [Header("Runtime Stats")]
    public int currentHP;

    private List<StatusEffect> activeEffects = new();

    private void Awake()
    {
        currentHP = characterInfo.maxHP;
    }

    // Called by BattleHandler each turn
    public void ProcessTurnEffects()
    {
        foreach (var effect in activeEffects.ToArray())
        {
            effect.OnTurn();
        }

        // Clean up expired (destroyed) effects
        activeEffects.RemoveAll(e => e == null);
    }

    // Apply a status effect (generic type)
    public void ApplyEffect<T>(int duration) where T : StatusEffect
    {
        T effect = gameObject.AddComponent<T>();
        effect.OnApply(this, duration);
        activeEffects.Add(effect);

        Debug.Log($"{characterInfo.characterName} is affected by {typeof(T).Name} for {duration} turn(s).");
    }

    // Damage this character
    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Max(currentHP, 0);

        Debug.Log($"{characterInfo.characterName} took {amount} damage. HP: {currentHP}/{characterInfo.maxHP}");

        if (IsDead())
        {
            Debug.Log($"{characterInfo.characterName} has been defeated!");
        }
    }

    // Heal this character
    public void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Min(currentHP, characterInfo.maxHP);

        Debug.Log($"{characterInfo.characterName} healed {amount} HP. HP: {currentHP}/{characterInfo.maxHP}");
    }

    // Dead check
    public bool IsDead()
    {
        return currentHP <= 0;
    }

    // Crude way to tell ally vs. enemy 
    public bool IsEnemy()
    {
        return transform.position.x > 0f;
    }
}
