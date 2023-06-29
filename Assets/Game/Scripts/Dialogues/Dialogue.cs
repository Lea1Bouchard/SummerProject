using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Dialogue")]
[System.Serializable]
public class Dialogue : ScriptableObject
{

    public string initiatorName;
    public bool isQuest;

    [TextArea(3, 10)]
    public string[] sentences;

    public void Subscribe()
    {
        if (isQuest)
            DialogueManager.OnEnd += QuestTrigger;
    }

    private void QuestTrigger()
    {
        DialogueManager.OnEnd -= QuestTrigger;

        //TODO : add quest events
    }
}
