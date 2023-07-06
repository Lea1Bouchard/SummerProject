using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(menuName = "Interactable/Interactable character")]
public class InteractableCharacter : Interactable
{
    [SerializeField] private NpcType npcType;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Dialogue questDialogue;
    [SerializeField] private List<Quest> quests;
    [SerializeField] private int currQuestIndex = 0;
    public bool isReadyToGiveQuest;

    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public override void Interact()
    {
        if (npcType == NpcType.QuestGiver)
        {
            TriggerDialogue(questDialogue);
        }
        else if (npcType == NpcType.Merchant)
        {
            //TODO : Do stuff if NPC is a merchant
        }
        else
        {
            TriggerDialogue(dialogue);
        }
    }

    private void TriggerDialogue(Dialogue dial)
    {
        DialogueManager.Instance.TriggerDialogue(dial);

        EventManager.Instance.QueueEvent(new TalkGameEvent(Name));
    }

    private void OpenShop()
    {

    }


}