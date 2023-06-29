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

    private void ChangeButton(GameObject button)
    {

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);
    }
}
