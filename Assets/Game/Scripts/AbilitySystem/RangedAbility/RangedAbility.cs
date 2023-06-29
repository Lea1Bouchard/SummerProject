using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/RangedAbility")]
public class RangedAbility : Ability
{
    public float timeToLive = 5f;
    public float speed = 1f;
    public float force = 1f;
    public GameObject projectile;
    private GameObject projectileClone;
    private Projectile projectileScript;

    public override void Initialize(Characters ini)
    {
        isActive = false;
        initiator = ini;
        animator = ini.GetComponent<Animator>();
        abilityType = Enums.TypeOfAbility.Ranged;
        abilityCooldownClass = initiator.gameObject.AddComponent<AbilityCooldown>();
    }

    public override void TriggerAbility()
    {
        if (CheckState())
        {
            projectileClone = Instantiate(projectile, initiator.transform.Find("ProjectileSpawn").position, initiator.transform.rotation);
            SetProjectileValues();
            projectileClone.GetComponent<Projectile>().Shoot(this, initiator.target);
            isActive = true;
            abilityCooldownClass.Initialize(this);
            Animate();
        }
    }


    public void ProjectileDestroyed()
    {
        Destroy(projectileClone);
    }

    private void SetProjectileValues()
    {
        projectileScript = projectileClone.GetComponent<Projectile>();

        projectileScript.Damage = baseDamage;
        projectileScript.Range = timeToLive;
        projectileScript.Speed = speed;
        projectileScript.Force = force;
        projectileScript.Initiator = Initiator;
        projectileScript.attackElement = attackElement;

    }
}
