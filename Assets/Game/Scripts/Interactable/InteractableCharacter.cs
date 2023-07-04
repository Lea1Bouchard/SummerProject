using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(menuName = "Interactable/Interactable character")]
public class InteractableCharacter : Interactable
{
    [SerializeField] private List<NpcType> npcType;
    [SerializeField] private Dialogue dialogue;
    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public override void Interact()
    {
        Debug.Log("Enemy type : " + TypeTranslate.Instance.TranslateEnemies(EnemyType.Dragon_NoWings));

        DialogueManager.Instance.TriggerDialogue(dialogue);
        EventManager.Instance.QueueEvent(new TalkGameEvent(Name));
    }
}
