using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverManager : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    public int linkedQuestIndex;
    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeData();
    }

    public void ChangeFocusedQuest()
    {
        CurrentObjectiveIndicator.Instance.ChangeFocusedQuest(QuestManager.Instance.currentQuests[linkedQuestIndex]);
    }

    public void OnSelect(BaseEventData eventData)
    {
        ChangeData();
    }

    private void ChangeData()
    {
        QuestDescriptionWindow.Instance.FillQuestData(QuestManager.Instance.currentQuests[linkedQuestIndex]);

        print("PointerEnter!! Quest index : " + linkedQuestIndex);

        print("Quest name : " + QuestManager.Instance.currentQuests[linkedQuestIndex].Information.name);
    }
}