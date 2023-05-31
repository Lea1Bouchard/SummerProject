using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/RangedAbility")]
public class RangedAbility : Ability
{
    public float range = 5f;
    public float speed = 1f;
    public GameObject projectile;
    public GameObject startPosition;
    private Projectile projectileScript;

    public override void Initialize()
    {
        projectileScript = projectile.GetComponent<Projectile>();

        Instantiate(projectile);

        projectileScript.Damage = baseDamage;
        projectileScript.Range = range;
        projectileScript.Speed = speed;
        projectileScript.Initiator = Initiator;
        projectileScript.attackElement = attackElement;
        abilityType = Enums.TypeOfAbility.ranged;

        //MABE :: Set material here?
    }

    public override void TriggerAbility()
    {
        if (CheckState())
        {
            projectileScript.Shoot(this);
            isActive = true;
        } else
        {
            Destroy(projectileScript.gameObject);
        }

    }

    public void ProjectileDestroyed()
    {
        isActive = false;
    }

}
