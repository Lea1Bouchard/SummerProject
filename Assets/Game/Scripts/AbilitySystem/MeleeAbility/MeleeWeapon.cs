using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class MeleeWeapon : MonoBehaviour
{
    public float damage;
    [SerializeField] private Characters owner;

    //Attack detected with trigger
    private void OnTriggerEnter(Collider other)
    {
        Characters hit = other.gameObject.GetComponentInParent<Characters>();
        if (hit != owner && hit != null)
        {
            foreach (Elements affinity in owner.Affinities)
            {
                hit.ReceiveDamage(affinity, damage);
            }
        }
    }

    //This is call in the animator
    //Easy way to tell when the weapon should and shouldn't deal damage
    public void Activate()
    {
        gameObject.GetComponent<Collider>().enabled = true;
    }
    //This is call in the animator
    public void Deactivate()
    {
        gameObject.GetComponent<Collider>().enabled = false;
    }

    //Used with ranged attack
    public void HideWeapon()
    {
        gameObject.SetActive(false);
    }

    public void ShowWeapon()
    {
        gameObject.SetActive(true);
    }
}
