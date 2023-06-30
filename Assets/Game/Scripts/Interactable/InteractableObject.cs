using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Interactable/Interactable object")]
public class InteractableObject : Interactable
{
    #region variables
    [SerializeField] private bool isQuestObject;
    #endregion
    public override void Initialize()
    {
        //TODO : Mabe initialize it with the quest name?
        throw new System.NotImplementedException();
    }

    public override void Interact()
    {
        if(isQuestObject)
        {
            NotifyQuest();
        } else
        {
            //TODO : Verify if this part is used (might not have an inventory system, quest item could be the only type of interactable items)
            Debug.Log("Item grabbed : " + Name);
        }
    }
    
    public void NotifyQuest()
    {
        //TODO : Send notification that a quest object has been added
    }
}
