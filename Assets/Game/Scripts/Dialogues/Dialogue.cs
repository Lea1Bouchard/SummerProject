using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Dialogue")]
[System.Serializable]
public class Dialogue : ScriptableObject
{
    #region variables
    public string initiatorName;
    public bool isQuest;
    [SerializeField] private Quest quest;

    [TextArea(3, 10)]
    public string[] sentences;
    #endregion

    //Subscribe to the end of dialogue event if this one is a quest dialogue
    public void Subscribe()
    {
        if (isQuest)
            DialogueManager.OnEnd += QuestTrigger;
    }

    private void QuestTrigger()
    {
        DialogueManager.OnEnd -= QuestTrigger;

        QuestWindow.Instance.Initialize(quest);
    }
}
