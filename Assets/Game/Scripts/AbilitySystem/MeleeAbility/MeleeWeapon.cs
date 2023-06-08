using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class MeleeWeapon : MonoBehaviour
{
    public bool isActive;
    public int damage;
    public Vector3 range;
    [HideInInspector] public Characters initiator;
    [HideInInspector] public Elements attackElement;

    private void OnTriggerEnter(Collider other)
    {
        Characters hit = other.gameObject.GetComponent<Characters>();
        if (isActive && hit != initiator && hit != null)
        {
            hit.ReceiveDamage(attackElement, damage);
        }

    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }

    public void HideWeapon()
    {
        gameObject.SetActive(false);
    }

    public void ShowWeapon()
    {
        gameObject.SetActive(true);
    }
}
