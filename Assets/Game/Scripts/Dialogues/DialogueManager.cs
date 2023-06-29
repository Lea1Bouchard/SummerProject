using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI DialogueText;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private GameObject questMarker;

    public delegate void DialogueEnd();
    public static event DialogueEnd OnEnd;

    private Queue<string> Sentences;

    private static DialogueManager _instance;

    public static DialogueManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("DialogueManager is NULL");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        Sentences = new Queue<string>();
    }

    public void TriggerDialogue(Dialogue dialogue)
    {
        GameManager.Instance.UpdateGameState(Enums.GameState.InMenu);

        dialogueCanvas.SetActive(true);

        if (dialogue.isQuest)
            questMarker.SetActive(true);
        else
            questMarker.SetActive(false);

        Cursor.lockState = CursorLockMode.None;

        StartDialogue(dialogue);
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
        GameManager.Instance.UpdateGameState(Enums.GameState.InGame);
    }

}
