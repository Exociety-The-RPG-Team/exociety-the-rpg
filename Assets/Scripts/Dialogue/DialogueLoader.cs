using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    public DialogueSystem dialogueSystem;

    void Start()
    {
        // Load JSON file from the Resources folder
        TextAsset jsonFile = Resources.Load<TextAsset>("dialogue");
        
        if (jsonFile != null)
        {
            dialogueSystem.LoadDialogueFromJson(jsonFile.text);
            dialogueSystem.StartDialogue();
        }
        else
        {
            Debug.LogError("Failed to load dialogue.json!");
        }
    }
}
