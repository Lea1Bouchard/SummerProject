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

            goalObj.GetComponent<GoalIndicator>().LinkedGoal = goal;
            goalsIndicators.Add(goalObj);
            goalObj.transform.position += (offset * -loopTimes);

            goalObj.GetComponent<GoalIndicator>().Initialize();

            UpdateListener(goalObj.GetComponent<GoalIndicator>(), goal);

            loopTimes++;
        }

        xpText.text = quest.reward.XP.ToString() + " XP";
        goldText.text = quest.reward.Currency.ToString() + " Gold";

        //Quest recieved noise
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

    private void UpdateListener(GoalIndicator indicator, Quest.QuestGoal goal)
    {
        switch (goal.goalType)
        {
            case Enums.GoalType.Bring:
                EventManager.Instance.AddListener<BringGameEvent>(delegate { indicator.UpdateGoalIndicator(); });
                break;      
                
            case Enums.GoalType.Fetch:
                EventManager.Instance.AddListener<FetchGameEvent>(delegate { indicator.UpdateGoalIndicator(); });
                break;

            case Enums.GoalType.Gather:
                //Game event not yet implemented
                //EventManager.Instance.AddListener<>(delegate { indicator.UpdateGoalIndicator(); });
                break;

            case Enums.GoalType.Slay:
                EventManager.Instance.AddListener<KillGameEvent>(delegate { indicator.UpdateGoalIndicator(); });
                break;

            case Enums.GoalType.Talk:
                EventManager.Instance.AddListener<TalkGameEvent>(delegate { indicator.UpdateGoalIndicator(); });
                break;
        }
    }
}
