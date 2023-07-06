using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverManager : MonoBehaviour, IPointerEnterHandler
{
    public int linkedQuestIndex;
    public void OnPointerEnter(PointerEventData eventData)
    {
        QuestDescriptionWindow.Instance.FillQuestData(QuestManager.Instance.currentQuests[linkedQuestIndex]);

        print("PointerEnter!! Quest index : " + linkedQuestIndex);

        print("Quest name : " + QuestManager.Instance.currentQuests[linkedQuestIndex].Information.name);
    }

    public void ChangeFocusedQuest()
    {
        CurrentObjectiveIndicator.Instance.ChangeFocusedQuest(QuestManager.Instance.currentQuests[linkedQuestIndex]);
    }
}
