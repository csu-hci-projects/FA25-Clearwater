using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    bool playerDetection = false;
    public TextPrinter textPrinter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(/*playerDetection && */Input.GetKeyDown(KeyCode.F)) {
            print("Dialogue started!");
            textPrinter.PrintText("Hello, traveler! Welcome to our village.");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.name == "Player") {
            playerDetection = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        playerDetection = false;
    }
}
