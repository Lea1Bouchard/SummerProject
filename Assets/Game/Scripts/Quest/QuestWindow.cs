using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private Transform goalsContent;
    [SerializeField] private Text xpText;
    [SerializeField] private Text goldText;
    [SerializeField] private AudioClip newQuestSound;

    private List<GameObject> goalsIndicators = new List<GameObject>();

    public void Initialize(Quest quest)
    {
        titleText.text = quest.Information.name;
        descriptionText.text = quest.Information.description;

        var offset = new Vector3(0, 25, 0);
        int loopTimes = 0;

        gameObject.SetActive(true);

        foreach (var goal in quest.Goals)
        {
            GameObject goalObj = Instantiate(goalPrefab, goalsContent);
            goalsIndicators.Add(goalObj);
            goalObj.transform.position += (offset * -loopTimes);
            goalObj.transform.Find("GoalDescription").GetComponent<TextMeshProUGUI>().text = goal.GetDescription();

            print(goal.GetDescription());

            if (goal.Completed)
            {
                goalObj.transform.Find("Done").gameObject.SetActive(true);
            }
            else if (goal.RequiredAmount > 1)
            {
                goalObj.transform.GetChild(0).Find("Amount").GetComponent<TextMeshProUGUI>().text = goal.CurrentAmount + " / " + goal.RequiredAmount;
            } 
            else
            {
                goalObj.transform.GetChild(0).Find("Amount").gameObject.SetActive(false);
            }

            loopTimes++;
        }

        print(quest.Goals.Count);

        xpText.text = quest.reward.XP.ToString() + " XP";
        goldText.text = quest.reward.Currency.ToString() + " Gold";

        //Quest recieve noise
        //AudioSource.PlayClipAtPoint(newQuestSound, new Vector3(0, 0, 0));

        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown(float countdownValue = 3f)
    {
        yield return new WaitForSeconds(countdownValue);
        gameObject.SetActive(false);
    }

    public void closeWindow()
    {
        gameObject.SetActive(false);

        for (int i = 0; i < goalsContent.childCount; i++)
        {
            Destroy(goalsContent.GetChild(i).gameObject);
        }
    }

}
