using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestlistButtonManager : MonoBehaviour
{

    [SerializeField] private GameObject scrollViewContent;
    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private GameObject returnButton;

    private void OnEnable()
    {
        FillButtons();
        //TODO : Mabe find a way to select back the previously selected button if select on right
        //Could use linkedQuestIndex in HoverManager
        gameObject.GetComponent<Menu>().ChangeButton(scrollViewContent.transform.GetChild(0).gameObject);

        Navigation buttonNav = new Navigation();
        buttonNav.mode = Navigation.Mode.None;
        buttonNav.mode = Navigation.Mode.Explicit;
        buttonNav.selectOnRight = scrollViewContent.transform.GetChild(0).gameObject.GetComponent<Button>();

        returnButton.GetComponent<Button>().navigation = buttonNav;
    }

    private void FillButtons()
    {
        int index = 0;
        foreach (Quest quest in QuestManager.Instance.currentQuests)
        {
            GameObject button = Instantiate(buttonTemplate);
            button.transform.SetParent(scrollViewContent.transform);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(quest.Information.name);
            button.GetComponent<HoverManager>().linkedQuestIndex = index;

            FillButtonNavigation(index, button);

            index++;
        }
    }

    private void FillButtonNavigation(int index, GameObject button)
    {
        Navigation buttonNav = new Navigation();
        buttonNav.mode = Navigation.Mode.Explicit;

        buttonNav.selectOnDown = scrollViewContent.transform.GetChild(0).gameObject.GetComponent<Button>();

        if (index != 0)
        {
            buttonNav.selectOnUp = scrollViewContent.transform.GetChild(index - 1).gameObject.GetComponent<Button>();
            buttonNav.selectOnDown = scrollViewContent.transform.GetChild(0).gameObject.GetComponent<Button>();

            //Sets the previous button to target the current one as a next
            Navigation previousButton = scrollViewContent.transform.GetChild(index - 1).gameObject.GetComponent<Button>().navigation;
            previousButton.selectOnDown = scrollViewContent.transform.GetChild(index).gameObject.GetComponent<Button>();

            Navigation firstButton = scrollViewContent.transform.GetChild(0).gameObject.GetComponent<Button>().navigation;
            firstButton.selectOnUp = scrollViewContent.transform.GetChild(index).gameObject.GetComponent<Button>();

            scrollViewContent.transform.GetChild(0).gameObject.GetComponent<Button>().navigation = firstButton;
            scrollViewContent.transform.GetChild(index - 1).gameObject.GetComponent<Button>().navigation = previousButton;
        }
        buttonNav.selectOnLeft = returnButton.GetComponent<Button>();
        button.GetComponent<Button>().navigation = buttonNav;
    }

    private void LoadQuestData()
    {

    }
}
