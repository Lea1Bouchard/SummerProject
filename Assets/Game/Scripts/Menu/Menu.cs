using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject firstButton;

    private void OnEnable()
    {
        if(firstButton)
            ChangeButton(firstButton);
    }

    public void ChangeButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);

        print("Button Changed");
    }
}
