using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private Transform goalsContent;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private AudioClip newQuestSound;

    private static QuestWindow _instance;

    private Vector3 offset = new Vector3(0, 25, 0);

    private List<GameObject> goalsIndicators = new List<GameObject>();

    public static QuestWindow Instance
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
    }

    public void Initialize(Quest quest)
    {
        titleText.text = quest.Information.name;
        descriptionText.text = quest.Information.description;

        gameObject.SetActive(true);

        //MIGHT BE USED TO SHOW QUEST OBJECTIVES
        /*
         
        int loopTimes = 0;
         
        foreach (var goal in quest.Goals)
        {
            GameObject goalObj = Instantiate(goalPrefab, goalsContent);

            goalObj.GetComponent<GoalIndicator>().LinkedGoal = goal;
            goalObj.transform.position += (offset * -loopTimes);

            goalObj.GetComponent<GoalIndicator>().Initialize();
            loopTimes++;
        }

        */

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

    
}
