using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalIndicator : MonoBehaviour
{
    private Quest.QuestGoal linkedGoal;

    public Quest.QuestGoal LinkedGoal { get => linkedGoal; set => linkedGoal = value; }

    public void Initialize()
    {
        transform.Find("GoalDescription").GetComponent<TextMeshProUGUI>().text = linkedGoal.GetDescription();

        if (linkedGoal.completed)
        {
            transform.Find("Done").gameObject.SetActive(true);
        }
        else if (linkedGoal.requiredAmount > 1)
        {
            transform.GetChild(0).Find("Amount").GetComponent<TextMeshProUGUI>().text = linkedGoal.currentAmount + " / " + linkedGoal.requiredAmount;
        }
        else
        {
            transform.GetChild(0).Find("Amount").gameObject.SetActive(false);
        }
    }

    public void UpdateGoalIndicator()
    {
        if (linkedGoal.completed)
        {
            //transform.Find("Done").gameObject.SetActive(true);
            transform.GetChild(0).Find("Amount").gameObject.SetActive(false);
        }
        else if (linkedGoal.requiredAmount > 1)
        {
            transform.GetChild(0).Find("Amount").GetComponent<TextMeshProUGUI>().text = linkedGoal.currentAmount + " / " + linkedGoal.requiredAmount;
        }
        else
        {
            transform.GetChild(0).Find("Amount").gameObject.SetActive(false);
        }
    }
}
