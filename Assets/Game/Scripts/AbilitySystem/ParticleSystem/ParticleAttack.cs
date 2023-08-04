using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAttack : MonoBehaviour
{
    public float damage;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Characters>() != null)
            other.gameObject.GetComponent<Characters>().CurrenthealthPoints -= damage;
    }
}
