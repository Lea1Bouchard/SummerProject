using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverManager : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    public int linkedQuestIndex;
    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeData();
    }

    public void ChangeFocusedQuest()
    {
        foreach (Transform child in transform.parent)
        {
            child.GetComponent<Button>().image.color = Color.white;
        }

        CurrentObjectiveIndicator.Instance.ChangeFocusedQuest(QuestManager.Instance.currentQuests[linkedQuestIndex]);

        gameObject.GetComponent<Button>().image.color = Color.yellow;

        CurrentObjectiveIndicator.Instance.gameObject.SetActive(false);
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