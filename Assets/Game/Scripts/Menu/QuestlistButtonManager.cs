using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestlistButtonManager : MonoBehaviour
{

    [SerializeField] private GameObject scrollViewContent;
    [SerializeField] private GameObject buttonTemplate;

    private void OnEnable()
    {
        FillButtons();

        gameObject.GetComponent<Menu>().ChangeButton(scrollViewContent.transform.GetChild(0).gameObject);

        //TEMP TEST
        GameManager.Instance.UpdateGameState(Enums.GameState.InMenu);
    }

    private void FillButtons()
    {
        int index = 0;
        foreach(Quest quest in QuestManager.Instance.CurrentQuests)
        {
            GameObject button = Instantiate(buttonTemplate);
            button.transform.SetParent(scrollViewContent.transform);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(quest.Information.name);
            button.GetComponent<HoverManager>().linkedQuestIndex = index;

            index++;
        }
    }

    private void LoadQuestData()
    {

    }
}
