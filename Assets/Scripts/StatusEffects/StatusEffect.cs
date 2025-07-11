using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    [HideInInspector] public BattleCharacter target;
    [HideInInspector] public int duration;

    public virtual void OnApply(BattleCharacter target, int duration)
    {
        this.target = target;
        this.duration = duration;
    }

    public abstract void OnTurn();

    public virtual void OnExpire()
    {
        Debug.Log($"{target.characterInfo.characterName}'s {this.GetType().Name} has expired.");
        Destroy(this);
    }
}
