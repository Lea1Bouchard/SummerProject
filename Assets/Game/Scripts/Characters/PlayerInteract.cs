using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private LayerMask interactableMask;

    public void Interact()
    {
        Collider[] interactables = Physics.OverlapSphere(transform.position, interactRange, interactableMask);

        if (interactables.Length > 0)
        {
            Debug.Log("Current object : " + interactables[0].gameObject.name);
            ClosestInteractable(interactables).StartInteract();
        }
            
    }

    private InteractingActor ClosestInteractable(Collider[] interactables)
    {
        float distance = 100;
        InteractingActor currentClosest = null;

        foreach (Collider interactCollider in interactables)
        {
            var currDist = Vector3.Distance(Player.Instance.transform.position + new Vector3(0, 1, 0), interactCollider.transform.position);

            if (currDist < distance)
            {
                distance = currDist;

                currentClosest = interactCollider.gameObject.GetComponent<InteractingActor>();
            }
        }

        return currentClosest;
    }
}
