using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Projectile : MonoBehaviour
{

    private int damage;
    private float speed;
    private float force;
    private float range;
    private Characters initiator;
    private RangedAbility abilityIninitator;
    public Rigidbody rigidb;
    [HideInInspector] public Elements attackElement;

    public int Damage { get => damage; set => damage = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Range { get => range; set => range = value; }
    public Characters Initiator { get => initiator; set => initiator = value; }
    public float Force { get => force; set => force = value; }

    // Start is called before the first frame update
    void Start()
    {
        rigidb = this.gameObject.GetComponent<Rigidbody>();
    }

    public void Shoot(RangedAbility ability, Characters target)
    {
        if (target != null)
        {
            transform.LookAt(target.gameObject.transform);
            rigidb.velocity = transform.forward * speed;
        } else
        {
            rigidb.useGravity = true;
            Vector3 direction = transform.forward + new Vector3(0, 1f, 0);
            rigidb.AddForce(direction * speed * force, ForceMode.Impulse);
        }

        abilityIninitator = ability;

        if (range > 0)
        StartCoroutine(rangeTimer());
    }

    IEnumerator rangeTimer()
    {
        yield return new WaitForSecondsRealtime(range);
        abilityIninitator.ProjectileDestroyed();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Characters hit = other.gameObject.GetComponent<Characters>();
        if(hit != initiator)
        {
            if (hit != null)
            {
                hit.ReceiveDamage(attackElement, damage);
                transform.LookAt(hit.transform);
                transform.parent = hit.transform;
            }

            if(range > 0)
            abilityIninitator.ProjectileDestroyed();
        }

    }

}
