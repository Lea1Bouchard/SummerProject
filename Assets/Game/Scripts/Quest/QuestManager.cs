using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questHolder;

    public List<Quest> CurrentQuests;
    [SerializeField] private int currentActiveQuest;

    private void Awake()
    {
        if (CurrentQuests[0] != null)
        {
            CurrentQuests[0].Initialize();
            CurrentQuests[0].QuestCompleted.AddListener(OnQuestCompleted);


            questHolder.SetActive(true);

        }
        currentActiveQuest = 0;

        questHolder.GetComponent<QuestWindow>().Initialize(CurrentQuests[currentActiveQuest]);
    }

    private void OnQuestCompleted(Quest quest)
    {
        print("quest completed");
        currentActiveQuest++;
        questHolder.GetComponent<QuestWindow>().closeWindow();

        if (CurrentQuests.Count == currentActiveQuest)
        {
            print("All quests are done");
            SceneManager.LoadScene(2);
            return;
        }
        print(CurrentQuests.Count);
        print(currentActiveQuest);

        CurrentQuests[currentActiveQuest].Initialize();
        CurrentQuests[currentActiveQuest].QuestCompleted.AddListener(OnQuestCompleted);

        questHolder.GetComponent<QuestWindow>().Initialize(CurrentQuests[currentActiveQuest]);
    }

    /*
    public void Slay(string killedEnemie)
    {
        EventManager.Instance.QueueEvent(new KillGameEvent(killedEnemie));
        print("Slayed");
    }
    */

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
