using UnityEngine;
using TMPro;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public TextMeshProUGUI dialogueText;

    public void DisplayDialogue(string message) {
        if(dialogueText != null) {
            dialogueText.text = message;
        }
        else {
            Debug.LogError("Dialogue TextMeshProUGUI not assigne in DialogueManager!");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayDialogue("Welcome to Clear Water!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}