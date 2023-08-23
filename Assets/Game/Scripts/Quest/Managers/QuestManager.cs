using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    private static QuestManager _instance;

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
        }
    }

    private void OnQuestCompleted(Quest quest)
    {
        currentQuests.Remove(quest);

        print("quest completed");

        if (currentQuests.Count == 0)
        {
            print("All quests are done");
            return;
        }
        print(currentQuests.Count);
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
