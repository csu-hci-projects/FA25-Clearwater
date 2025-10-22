using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPrinter : MonoBehaviour
{

    private TMP_Text subtitleTextMesh;
    private Coroutine typingCoroutine;

    void Awake()
    {
        subtitleTextMesh = GetComponent<TMP_Text>();
    }

    public void PrintText(string line) {
        if(typingCoroutine != null) {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeTextCO(line));
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private IEnumerator TypeTextCO(string textToType)
    {
        subtitleTextMesh.text = string.Empty;

        for(int i = 0; i < textToType.Length; i++)
        {
            subtitleTextMesh.text += textToType[i];
            yield return new WaitForSeconds(0.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
