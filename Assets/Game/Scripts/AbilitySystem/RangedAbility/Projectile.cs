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
    private bool isMoving;

    public int Damage { get => damage; set => damage = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Range { get => range; set => range = value; }
    public Characters Initiator { get => initiator; set => initiator = value; }
    public float Force { get => force; set => force = value; }

    // Start is called before the first frame update
    void Start()
    {
        rigidb = GetComponent<Rigidbody>();
    }

    public void Shoot(RangedAbility ability, Characters target)
    {
        if (target != null)
        {
            transform.LookAt(target.targetLocation.gameObject.transform);
            rigidb.velocity = transform.forward * speed;
        }
        else
        {
            rigidb.useGravity = true;
            Vector3 direction = transform.forward + new Vector3(0, 0.5f, 0);
            rigidb.AddForce(direction * force, ForceMode.Impulse);
        }
        
        abilityIninitator = ability;
        isMoving = true;

        if (range > 0)
            StartCoroutine(RangeTimer());

        StartCoroutine(AngleAjustment());
    }

    IEnumerator RangeTimer()
    {
        yield return new WaitForSeconds(range);
        abilityIninitator.ProjectileDestroyed();

    }    

    IEnumerator AngleAjustment()
    {
        while(isMoving)
        {
            yield return new WaitForSeconds(0);
            AjustAngle();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Characters hit = other.gameObject.GetComponent<Characters>();
        if (hit != null && hit != initiator && other.GetComponent<NotifyZone>() == null)
        {
            if (hit != null)
            {
                hit.ReceiveDamage(attackElement, damage);

                if (initiator.gameObject.GetComponent<Player>())
                    ProjectileStick(GetClosestBone(hit, other.ClosestPoint(transform.position)));
            }

            if (range > 0)
                abilityIninitator.ProjectileDestroyed();
        } 
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isMoving = false;
            gameObject.GetComponent<Collider>().enabled = false;
            Destroy(rigidb);
        }
    }
    private void ProjectileStick(Transform bone)
    {
        isMoving = false;
        transform.LookAt(bone);
        transform.position = bone.position;
        transform.parent = bone;

        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(rigidb);
    }

    private Transform GetClosestBone(Characters hitChar, Vector3 hitPosition)
    {

        float closestPos = 100;
        Transform closestBone = null;

        foreach (Transform child in hitChar.weaponNodes)
        {
            if (Vector3.Distance(child.position, hitPosition) < closestPos)
            {
                closestPos = Vector3.Distance(child.position, hitPosition);
                closestBone = child;
            }
        }

        return closestBone;
    }

    private void AjustAngle()
    {
        float angle = Mathf.Atan2(rigidb.velocity.y, Mathf.Abs(rigidb.velocity.z)) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(Quaternion.AngleAxis(angle, -Vector3.right).eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void OnDestroy()
    {
        if(initiator.gameObject.GetComponent<Player>())
        {
            initiator.SetTeleportTarget(null);
        }

        StopAllCoroutines();
    }

}
