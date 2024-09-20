using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro
using UnityEngine.UI; // Import UI for Image component

[System.Serializable]
public class Dialogue
{
    public string character;
    public string[] lines;
    public int emotionID; // Add a field for the character icon
    public CharacterData characterData;
}

[System.Serializable]
public class DialogueContainer
{
    public Dialogue[] dialogues;
}

public class DialogueSystem : MonoBehaviour
{
    public DialogueContainer dialogueContainer; // Holds parsed JSON data

    // References to the UI elements
    public TextMeshProUGUI characterNameText; // TextMeshPro for the character name
    public TextMeshProUGUI dialogueText; // TextMeshPro for the dialogue lines
    public Image characterIcon; // UI Image for the character's icon

    private int currentDialogueIndex = 0; // To track the current dialogue
    private int currentLineIndex = 0; // To track the current line within a dialogue

    public void LoadDialogueFromJson(string json)
    {
        dialogueContainer = JsonUtility.FromJson<DialogueContainer>(json);
    }

    // Function to start the dialogue and display the first character's dialogue
    public void StartDialogue()
    {
        currentDialogueIndex = 0;
        currentLineIndex = 0;
        DisplayDialogue();
    }

    // Function to display the current dialogue on the UI
    public void DisplayDialogue()
    {
        if (currentDialogueIndex < dialogueContainer.dialogues.Length)
        {
            Dialogue currentDialogue = dialogueContainer.dialogues[currentDialogueIndex];

            // Update the UI elements with the current dialogue
            characterNameText.text = currentDialogue.character;
            dialogueText.text = currentDialogue.lines[currentLineIndex];

            if (currentDialogue.emotionID != null)
            {
                characterIcon.sprite = currentDialogue.characterData.emotionsIcons[currentDialogue.emotionID]; // Update the character's icon
            }

            // Advance to the next line within the same dialogue
            currentLineIndex++;
            if (currentLineIndex >= currentDialogue.lines.Length)
            {
                currentDialogueIndex++; // Move to the next dialogue if there are no more lines
                currentLineIndex = 0;
            }
        }
    }

    // Optional: A function to proceed to the next line or dialogue when an action occurs
    public void NextDialogue()
    {
        if (currentDialogueIndex < dialogueContainer.dialogues.Length)
        {
            DisplayDialogue();
        }
        else
        {
            Debug.Log("End of dialogue");
        }
    }
}
