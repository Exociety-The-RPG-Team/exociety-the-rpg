using UnityEngine;

public class PoisonEffect : StatusEffect
{
    public int damagePerTurn = 5;

    public override void OnTurn()
    {
        if (target == null || target.IsDead())
            return;

        Debug.Log($"{target.characterInfo.characterName} takes {damagePerTurn} poison damage!");
        target.TakeDamage(damagePerTurn);

        duration--;
        if (duration <= 0)
            OnExpire();
    }
}
