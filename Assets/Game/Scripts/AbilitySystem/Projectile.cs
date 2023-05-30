using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private int damage;
    private float speed;
    private float range;
    private Characters initiator;
    private Rigidbody rigidb;

    public int Damage { get => damage; set => damage = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Range { get => range; set => range = value; }
    public Characters Initiator { get => initiator; set => initiator = value; }

    // Start is called before the first frame update
    void Start()
    {
        rigidb = this.gameObject.GetComponent<Rigidbody>();
    }

    public void Shoot()
    {
        rigidb.velocity = transform.forward * speed;
        Destroy(gameObject, range);
    }


}
