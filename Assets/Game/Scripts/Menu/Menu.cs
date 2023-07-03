using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject firstButton, newCurrentButton;

    private void OnEnable()
    {
        ChangeButton(firstButton);
    }
    //Changes the current selected button in a menu
    //SHOULD BE USED WITH THE MANUAL BUTTON LINKS IN THE MENU
    private void ChangeButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);
    }
}
