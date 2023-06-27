using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text DialogueText;
    public GameObject dialogueCanvas;

    public delegate void DialogueEnd();
    public static event DialogueEnd OnEnd;

    private Queue<string> Sentences;

    void Start()
    {
        Sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Sentences.Clear();

        dialogue.Subscribe();

        foreach (string sentence in dialogue.sentences)
        {
            Sentences.Enqueue(sentence);
        }

        nameText.text = dialogue.name;

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (Sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = Sentences.Dequeue();

        DialogueText.text = sentence;

    }

    public void EndDialogue()
    {
        if (OnEnd != null)
            OnEnd();

        Cursor.lockState = CursorLockMode.Locked;
        dialogueCanvas.SetActive(false);
    }

}
