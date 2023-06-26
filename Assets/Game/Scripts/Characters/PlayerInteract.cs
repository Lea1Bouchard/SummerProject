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

        ClosestInteractable(interactables).Interact();
    }

    private Interactable ClosestInteractable(Collider[] interactables)
    {
        float distance = interactRange;
        Interactable currentClosest = null;

        foreach (Collider interactCollider in interactables)
        {
            var currDist = Vector3.Distance(Player.Instance.transform.position + new Vector3(0, 1, 0), interactCollider.transform.position);

            if (currDist < distance)
            {
                distance = currDist;

                currentClosest = interactCollider.GetComponent<Interactable>();
            }
        }

        return currentClosest;
    }
}
