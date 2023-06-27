using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text DialogueText;
    public GameObject dialogueCanvas;

    private Queue<string> Sentences;
    //TODO : add the quest manager so they can communicate
    //private QuestManager questManager;

    private void Awake()
    {
        //questManager = GameObject.FindGameObjectsWithTag("QuestManager")[0].GetComponent<QuestManager>();
    }

    void Start()
    {
        Sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Sentences.Clear();

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
        /*
        if (nameText.text == "Pand-Ah")
            questManager.Talked("Panda");
        */
        Cursor.lockState = CursorLockMode.Locked;
        dialogueCanvas.SetActive(false);
    }

}
