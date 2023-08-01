using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAttack : MonoBehaviour
{
    public float damage;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Characters>())
        {
            collision.gameObject.GetComponent<Characters>().CurrenthealthPoints -= damage;
            Debug.Log(collision.gameObject.GetComponent<Characters>().CurrenthealthPoints);
        }
    }
}
