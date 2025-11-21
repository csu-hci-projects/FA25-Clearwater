using UnityEngine;
using TMPro;
using System.Collections;


public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;

    private TextPrinter textPrinter;
    public bool PlayerDetection { get; private set; }
    public bool DialogueRunning { get; private set; }

    private void Start()
    {
        PlayerDetection = false;
        DialogueRunning = false;

        textPrinter = GetComponent<TextPrinter>();
        CloseDialogueBox();
    }


    private void Update()
    {
        if (PlayerDetection && !DialogueRunning && Input.GetKeyDown(KeyCode.F))
        {
            ShowDialogue(testDialogue);
        }
    }

    public void SetDialogueObject(DialogueObject dialogueObject) => testDialogue = dialogueObject;

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        DialogueRunning = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }


    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach(string dialogue in dialogueObject.Dialogue)
        {

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            yield return null;

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        }

        CloseDialogueBox();
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        textPrinter.Run(dialogue, textLabel);

        while(textPrinter.IsRunning)
        {
            yield return null;

            if(Input.GetKeyDown(KeyCode.F))
            {
                textPrinter.Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            PlayerDetection = true;
        }
    }


    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            PlayerDetection = false;
        }
    }


    private void CloseDialogueBox()
    {
        DialogueRunning = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
