using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(menuName = "Interactable/Interactable character")]
public class InteractableCharacter : Interactable
{
    [SerializeField] private List<NpcType> npcType;
    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public override void Interact()
    {
        Debug.Log("Dialogue system here!!");
    }
}
