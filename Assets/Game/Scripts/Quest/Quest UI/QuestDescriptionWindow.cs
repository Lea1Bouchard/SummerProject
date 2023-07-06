using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDescriptionWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI exp;
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private Transform goalContainer;

    private static QuestDescriptionWindow _instance;

    public static QuestDescriptionWindow Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("QuestDescriptionWindow is NULL");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        gameObject.SetActive(false);
    }

    public void FillQuestData(Quest quest)
    {
        title.text = quest.Information.name;
        description.text = quest.Information.description;
        gold.text = quest.reward.Currency.ToString();
        exp.text = quest.reward.XP.ToString();

        var offset = new Vector3(0, 25, 0);
        int loopTimes = 0;

        DestroyGoal();

        foreach (var goal in quest.Goals)
        {
            GameObject goalObj = Instantiate(goalPrefab, goalContainer);

            goalObj.GetComponent<GoalIndicator>().LinkedGoal = goal;
            goalObj.transform.position += (offset * -loopTimes);

            goalObj.GetComponent<GoalIndicator>().Initialize();

            loopTimes++;
        }
    }    

    public void DestroyGoal()
    {
        foreach(Transform goal in goalContainer)
        {
            Destroy(goal.gameObject);
        }
    }
}
