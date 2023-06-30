using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(menuName = "Interactable/Interactable character")]
public class InteractableCharacter : Interactable
{
    #region variable
    [SerializeField] private List<NpcType> npcType;
    [SerializeField] private Dialogue dialogue;
    #endregion
    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public override void Interact()
    {
        DialogueManager.Instance.TriggerDialogue(dialogue);
    }
}
