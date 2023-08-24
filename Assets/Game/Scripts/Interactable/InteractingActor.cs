using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingActor : MonoBehaviour
{
    #region variables
    [SerializeField] private Interactable interactable;
    #endregion
    void Start()
    {
        //Nothing to do here yet
        //interactable.Initialize();
    }

    public void StartInteract()
    {
        interactable.Interact();
    }
}
