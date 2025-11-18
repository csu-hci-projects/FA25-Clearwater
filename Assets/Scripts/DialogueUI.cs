using UnityEngine;
using TMPro;
using System.Collections;


public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;

    private TextPrinter textPrinter;
    private bool playerDetection = false;
    private bool dialogueRunning = false;

    private void Start()
    {
        textPrinter = GetComponent<TextPrinter>();
        CloseDialogueBox();
    }


    private void Update()
    {
        if (playerDetection && !dialogueRunning && Input.GetKeyDown(KeyCode.F))
        {
            ShowDialogue(testDialogue);
        }
    }


    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueRunning = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }


    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach(string dialogue in dialogueObject.Dialogue)
        {
            yield return textPrinter.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        CloseDialogueBox();
    }


    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            playerDetection = true;
        }
    }


    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            playerDetection = false;
        }
    }


    private void CloseDialogueBox()
    {
        dialogueRunning = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
