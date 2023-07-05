using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverManager : MonoBehaviour, IPointerEnterHandler
{
    public int linkedQuestIndex;
    public void OnPointerEnter(PointerEventData eventData)
    {
        QuestDescriptionWindow.Instance.FillQuestData(QuestManager.Instance.CurrentQuests[linkedQuestIndex]);

        print("PointerEnter!! Quest index : " + linkedQuestIndex);

        print("Quest name : " + QuestManager.Instance.CurrentQuests[linkedQuestIndex].Information.name);
    }

}
