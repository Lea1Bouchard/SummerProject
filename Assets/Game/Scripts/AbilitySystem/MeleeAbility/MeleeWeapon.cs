using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class MeleeWeapon : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isActive;
    public int damage;
    [HideInInspector] public Characters initiator;
    public Elements attackElement;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Characters hit = other.gameObject.GetComponent<Characters>();

        if (isActive && hit != initiator)
        {
            hit.ReceiveDamage(attackElement, damage);
        }
    }
}
