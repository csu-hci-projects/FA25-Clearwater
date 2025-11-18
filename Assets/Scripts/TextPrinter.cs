using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class TextPrinter : MonoBehaviour
{

    [SerializeField][Range(0,0.5f)] float normalTextSpeed;
    [SerializeField][Range(0,0.5f)] float skipTextSpeed;

    float currentTextSpeed;

    private TMP_Text textMesh;
    private Coroutine typingCoroutine;

    void Awake()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    /*public void PrintText(string line) {
        if(typingCoroutine != null) {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeTextCO(line));
    }*/

    public void Print(List<string> sentences, Action onFinishedPrinting)
    {
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(PrintDialogue(sentences, onFinishedPrinting));
    }

    IEnumerator PrintDialogue(List<string> sentences, Action onFinishedPrinting)
    {
        foreach(var sentence in sentences)
        {
            textMesh.text = string.Empty;
            yield return new WaitForSeconds(0.1f);

            foreach(var letter in sentence)
            {
                HandleTextSpeed();
                textMesh.text += letter;
                if(letter == ' ') continue;
                yield return new WaitForSecondsRealtime(currentTextSpeed);
            }
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.E));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }
        textMesh.text = string.Empty;
        onFinishedPrinting?.Invoke();
    }

    /*void RepositionSentence(string sentence)
    {
        textMesh.text = sentence;
        textMesh.ForceMeshUpdate();
        var firstChar = textMesh.textInfo.lineInfo[0].firstVisibleCharacterIndex;
        var lastChar = textMesh.textInfo.lineInfo[0].lastVisisbleCharacterIndex;
        var firstCharPos = textMesh.textInfo.characterInfo[firstChar].topLeft;
        var lastCharPos = textMesh.textInfo.characterInfo[lastChar].topRight;
        textMesh.rectTransfrom.anchoredPosition = new Vector2(0 - ((firstCharPos.x + lastCharPos.x) / 2), textMesh.reactTransform.anchoredPosition);
        textMesh.text = string.Empty;
    }*/

    void HandleTextSpeed()
    {
        if(Input.GetKey(KeyCode.E))
        {

            currentTextSpeed = skipTextSpeed;
        }
        else{
            currentTextSpeed = normalTextSpeed;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private IEnumerator TypeTextCO(string textToType)
    {
        textMesh.text = string.Empty;

        for(int i = 0; i < textToType.Length; i++)
        {
            textMesh.text += textToType[i];
            yield return new WaitForSeconds(0.025f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
