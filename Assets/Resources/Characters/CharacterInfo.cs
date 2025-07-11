using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterInfo", menuName = "Custom/Character Info")]
public class CharacterInfo : ScriptableObject
{
    public string characterName = "Unnamed";
    public int maxHP = 100;

    
    public int baseAttack = 10;
    public int baseDefense = 5;
}
