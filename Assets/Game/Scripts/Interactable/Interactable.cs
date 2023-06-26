using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : ScriptableObject
{
    public string Name;

    //TODO : Add event when interacted with to tell everyone (Most likely used for Quest system)

    public abstract void Initialize();
    public abstract void Interact();
}