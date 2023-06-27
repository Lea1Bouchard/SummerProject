using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject dialogueCanvas;

    public void TriggerDialogue()
    {
        dialogueCanvas.SetActive(true);

        Cursor.lockState = CursorLockMode.None;

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
