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
            textPrinter.PrintText("Hiya! Did you see the lake lately? It's been getting mighty dirty. What a shame.");
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
