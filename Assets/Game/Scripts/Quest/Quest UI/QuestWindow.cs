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
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private AudioClip newQuestSound;

    private Quest requestedQuest;

    private static QuestWindow _instance;

    public static QuestWindow Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("QuestWindow is NULL");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        gameObject.SetActive(false);
    }

    public void Initialize(Quest quest)
    {
        requestedQuest = quest;
        titleText.text = quest.Information.name;
        descriptionText.text = quest.Information.description;
        xpText.text = quest.reward.XP.ToString() + " XP";
        goldText.text = quest.reward.Currency.ToString() + " Gold";

        gameObject.SetActive(true);

        //Quest recieved noise
        //AudioSource.PlayClipAtPoint(newQuestSound, new Vector3(0, 0, 0));
    }

    public void closeWindow()
    {

        gameObject.SetActive(false);
        requestedQuest = null;
        GameManager.Instance.UpdateGameState(Enums.GameState.InGame);
    }

    public void AcceptQuest()
    {
        QuestManager.Instance.AddQuest(requestedQuest);

        closeWindow();
    }

    public void RefuseQuest()
    {
        closeWindow();
    }

}