using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    private static QuestManager _instance;
    [SerializeField] private GameObject questHolder;

    public List<Quest> currentQuests;

    public static QuestManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("QuestManager is NULL");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        if (currentQuests.Count > 0)
        {
            foreach (Quest quest in currentQuests)
            {
                quest.Initialize();
                quest.QuestCompleted.AddListener(OnQuestCompleted);
            }

            questHolder.GetComponent<QuestWindow>().Initialize(currentQuests[0]);
            questHolder.SetActive(true);
        }
    }

    private void OnQuestCompleted(Quest quest)
    {
        currentQuests.Remove(quest);

        print("quest completed");
        questHolder.GetComponent<QuestWindow>().closeWindow();

        if (currentQuests.Count == 0)
        {
            print("All quests are done");
            return;
        }
        print(currentQuests.Count);

        //TODO : Check how we can cycle trough quests
        //questHolder.GetComponent<QuestWindow>().Initialize(CurrentQuests[0]);
    }

    public void AddQuest(Quest quest)
    {
        currentQuests.Add(quest);
    }

    
    public void Slay(Enums.EnemyType killedEnemie)
    {
        EventManager.Instance.QueueEvent(new KillGameEvent(killedEnemie));
        print("Slayed");
    }

    public void Fetched(string fetchedItem)
    {
        EventManager.Instance.QueueEvent(new FetchGameEvent(fetchedItem));
        print("Fetched!");
    }
    public void Talked(string personTalkedTo)
    {
        EventManager.Instance.QueueEvent(new TalkGameEvent(personTalkedTo));
        print("Talked!");
    }

    public void Brought(string itemBrought, string location)
    {
        EventManager.Instance.QueueEvent(new BringGameEvent(itemBrought, location));
        print("Brought!");
    }
}
