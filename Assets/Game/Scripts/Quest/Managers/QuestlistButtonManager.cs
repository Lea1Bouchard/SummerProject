using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestlistButtonManager : MonoBehaviour
{

    [SerializeField] private GameObject scrollViewContent;
    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private Button returnButton;

    private void Awake()
    {
        Navigation buttonNav = returnButton.GetComponent<Button>().navigation;
        buttonNav.selectOnRight = scrollViewContent.transform.GetChild(0).gameObject.GetComponent<Button>();
    }

    private void OnEnable()
    {
        FillButtons();
        //TODO : Mabe find a way to select back the previously selected button if select on right
        //Could use linkedQuestIndex in HoverManager
        gameObject.GetComponent<Menu>().ChangeButton(scrollViewContent.transform.GetChild(0).gameObject);
    }

    private void FillButtons()
    {
        int index = 0;
        foreach(Quest quest in QuestManager.Instance.currentQuests)
        {
            GameObject button = Instantiate(buttonTemplate);
            button.transform.SetParent(scrollViewContent.transform);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(quest.Information.name);
            button.GetComponent<HoverManager>().linkedQuestIndex = index;

            Navigation buttonNav = button.GetComponent<Button>().navigation;

            FillButtonNavigation(index, buttonNav);

            index++;
        }
    }

    private void FillButtonNavigation(int index, Navigation buttonNav)
    {
        //Resets and sets the nav mode of the button to explicit
        buttonNav.mode = Navigation.Mode.None;
        buttonNav.mode = Navigation.Mode.Explicit;

        if (index != 0)
        {
            buttonNav.selectOnUp = scrollViewContent.transform.GetChild(index - 1).gameObject.GetComponent<Button>();
        }

        buttonNav.selectOnDown = scrollViewContent.transform.GetChild(index = 1).gameObject.GetComponent<Button>();
        buttonNav.selectOnLeft = returnButton;
    }

    private void LoadQuestData()
    {

    }
}
