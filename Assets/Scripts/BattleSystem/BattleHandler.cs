using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    public GameObject[] alliesToInit;
    public GameObject[] enemiesToInit;

    private List<BattleCharacter> allies = new();
    private List<BattleCharacter> enemies = new();

    private int currentTurnIndex = 0;
    private List<BattleCharacter> turnOrder = new();

    private void Start()
    {
        SpawnCharacters();
        StartCoroutine(BattleLoop());
    }

    private void SpawnCharacters()
    {
        Vector3 allyStartPos = new Vector3(-4, 0, 0);
        Vector3 enemyStartPos = new Vector3(4, 0, 0);
        float spacing = 1.5f;

        Transform allyParent = new GameObject("Allies").transform;
        Transform enemyParent = new GameObject("Enemies").transform;

        for (int i = 0; i < alliesToInit.Length; i++)
        {
            Vector3 position = allyStartPos + new Vector3(0, -i * spacing, 0);
            GameObject obj = Instantiate(alliesToInit[i], position, Quaternion.identity, allyParent);
            var character = obj.GetComponent<BattleCharacter>();
            allies.Add(character);
            turnOrder.Add(character);
        }

        for (int i = 0; i < enemiesToInit.Length; i++)
        {
            Vector3 position = enemyStartPos + new Vector3(0, -i * spacing, 0);
            GameObject obj = Instantiate(enemiesToInit[i], position, Quaternion.identity, enemyParent);
            var character = obj.GetComponent<BattleCharacter>();
            enemies.Add(character);
            turnOrder.Add(character);
        }
    }

    private IEnumerator BattleLoop()
    {
        yield return new WaitForSeconds(1f); // wait for spawn

        while (true)
        {
            if (CheckBattleOver())
                yield break;

            BattleCharacter current = turnOrder[currentTurnIndex];
            if (current == null || current.IsDead())
            {
                AdvanceTurn();
                continue;
            }

            Debug.Log($"▶️ {current.characterInfo.characterName}'s turn!");

            // Process status effects (like poison tick)
            current.ProcessTurnEffects();

            yield return new WaitForSeconds(0.5f);

            // Simulate action (e.g. attack random enemy)
            yield return StartCoroutine(SimulateAction(current));

            yield return new WaitForSeconds(1f);

            AdvanceTurn();
        }
    }

    private IEnumerator SimulateAction(BattleCharacter attacker)
    {
        List<BattleCharacter> targets = attacker.IsEnemy() ? allies : enemies;
        targets.RemoveAll(t => t == null || t.IsDead());

        if (targets.Count == 0)
            yield break;

        BattleCharacter target = targets[Random.Range(0, targets.Count)];

        int damage = 10; // hardcoded for now
        Debug.Log($"{attacker.characterInfo.characterName} attacks {target.characterInfo.characterName} for {damage} damage!");
        target.TakeDamage(damage);

        yield return null;
    }

    private void AdvanceTurn()
    {
        currentTurnIndex = (currentTurnIndex + 1) % turnOrder.Count;
    }

    private bool CheckBattleOver()
    {
        bool allEnemiesDead = enemies.TrueForAll(e => e == null || e.IsDead());
        bool allAlliesDead = allies.TrueForAll(a => a == null || a.IsDead());

        if (allEnemiesDead)
        {
            Debug.Log("✅ Victory!");
            return true;
        }

        if (allAlliesDead)
        {
            Debug.Log("❌ Defeat!");
            return true;
        }

        return false;
    }
}
