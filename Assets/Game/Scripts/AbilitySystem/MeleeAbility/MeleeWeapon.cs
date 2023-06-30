using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class MeleeWeapon : MonoBehaviour
{
    #region variables
    public int damage;
    public Vector3 range;
    [HideInInspector] public Characters initiator;
    [HideInInspector] public Elements attackElement;
    #endregion

    //Attack detected with trigger
    private void OnTriggerEnter(Collider other)
    {
        Characters hit = other.gameObject.GetComponent<Characters>();
        if (hit != initiator && hit != null)
        {
            hit.ReceiveDamage(attackElement, damage);
        }


        Debug.Log(other.gameObject);
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
