using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingActor : MonoBehaviour
{
    [SerializeField] private Interactable interactable;

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
