using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class representing each dialogue entry
[System.Serializable]
public class Dialogue
{
    public string character;
    public string[] lines;
}

// A class representing the dialogue container (root of the JSON structure)
[System.Serializable]
public class DialogueContainer
{
    public Dialogue[] dialogues;
}

public class DialogueSystem : MonoBehaviour
{
    // This will store the parsed dialogues
    public DialogueContainer dialogueContainer;

    // Load and parse the JSON data
    public void LoadDialogueFromJson(string json)
    {
        dialogueContainer = JsonUtility.FromJson<DialogueContainer>(json);
    }

    // Example function to start the dialogue system
    public void StartDialogue()
    {
        foreach (Dialogue dialogue in dialogueContainer.dialogues)
        {
            Debug.Log($"Character: {dialogue.character}");
            foreach (string line in dialogue.lines)
            {
                Debug.Log($"{dialogue.character}: {line}");
            }
        }
    }
}
