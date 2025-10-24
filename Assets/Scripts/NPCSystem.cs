using System.Collections.Generic;
using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    bool playerDetection = false;
    public TextPrinter textPrinter;
    List<string> gpaDialogue = new List<string>();

    string line1 = "Hey kiddo! Lotta bodies in the lake again. Gett'n to be that time o' year I guess.";
    string line2 = "Say, that reminds me of when they found your gam gam in there.";
    string line3 = "Anywho... them bodies ain't gon' fish themselves out. Better get a move on.";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*gpaDialogue.Add(line1);
        gpaDialogue.Add(line2);
        gpaDialogue.Add(line3);*/
    }

    // Update is called once per frame
    void Update()
    {
        if(playerDetection && Input.GetKeyDown(KeyCode.F)) {
            gpaDialogue.Add(line1);
            gpaDialogue.Add(line2);
            gpaDialogue.Add(line3);
            print("Dialogue started!");
            textPrinter.Print(gpaDialogue, OnDialogueFinished);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            playerDetection = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        playerDetection = false;
        gpaDialogue.Clear();
    }

    private void OnDialogueFinished() {
        print("Grandpa finished talking!");
    }
}
