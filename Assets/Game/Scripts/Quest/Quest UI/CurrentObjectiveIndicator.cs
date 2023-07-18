using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentObjectiveIndicator : MonoBehaviour
{

    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private Transform goalsContent;
    private static CurrentObjectiveIndicator _instance;

    private Vector3 offset = new Vector3(0, 25, 0);

    public static CurrentObjectiveIndicator Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("CurrentObjectiveIndicator is NULL");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        if (transform.childCount <= 0 && QuestManager.Instance.currentQuests.Count > 0)
        {
            LoadGoals(QuestManager.Instance.currentQuests[0]);
        }
    }

    private void LoadGoals(Quest quest)
    {
        int loopTimes = 0;

        gameObject.SetActive(true);

        foreach (var goal in quest.Goals)
        {
            GameObject goalObj = Instantiate(goalPrefab, goalsContent);

            goalObj.GetComponent<GoalIndicator>().LinkedGoal = goal;
            goalObj.transform.position += (offset * -loopTimes);

            goalObj.GetComponent<GoalIndicator>().Initialize();

            UpdateListener(goalObj.GetComponent<GoalIndicator>(), goal);

            loopTimes++;
        }
    }

    public void ChangeFocusedQuest(Quest quest)
    {
        DestroyGoal();

        LoadGoals(quest);
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

    public void DestroyGoal()
    {
        foreach (Transform goal in goalsContent)
        {
            Destroy(goal.gameObject);
        }
    }
}
