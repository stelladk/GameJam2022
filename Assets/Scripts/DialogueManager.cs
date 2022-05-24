using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] DialoguePanel dialoguePanel;
    public Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            NextSentence();
        }
    }

    public void StartDialogue(string[] speechList)
    {
        foreach(string speech in speechList){
            sentences.Enqueue(speech);
        }

        NextSentence();
    }

    public void NextSentence()
    {
        if (sentences.Count == 0){
            dialoguePanel.endDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialoguePanel.createDialogue(sentence, null, 0);
    }
}
