using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Projectile : MonoBehaviour
{

    private int damage;
    private float speed;
    private float range;
    private Characters initiator;
    private RangedAbility abilityIninitator;
    private Rigidbody rigidb;
    [HideInInspector] public Elements attackElement;

    public int Damage { get => damage; set => damage = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Range { get => range; set => range = value; }
    public Characters Initiator { get => initiator; set => initiator = value; }

    // Start is called before the first frame update
    void Start()
    {
        rigidb = this.gameObject.GetComponent<Rigidbody>();
    }

    public void Shoot(RangedAbility ability)
    {
        rigidb.velocity = transform.forward * speed;
        abilityIninitator = ability;
        Destroy(gameObject, range);
    }

    private void OnTriggerEnter(Collider other)
    {
        Characters hit = other.gameObject.GetComponent<Characters>();
        if (hit != initiator)
        {
            hit.DamageTaken(attackElement, damage);
        }

        Destroy(gameObject);

    }

    private void OnDestroy()
    {
        abilityIninitator.ProjectileDestroyed();
    }

}
