using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/RangedAbility")]
public class RangedAbility : Ability
{
    #region variables
    public float timeToLive = 5f;
    public float targettedSpeed = 1f;
    public float untargettedForce = 1f;
    public GameObject projectile;
    private GameObject projectileClone;
    private Projectile projectileScript;
    #endregion

    public override void Initialize(Characters ini)
    {
        isActive = false;
        initiator = ini;
        animator = ini.GetComponent<Animator>();
        abilityType = Enums.TypeOfAbility.Ranged;
        abilityCooldownClass = initiator.gameObject.AddComponent<AbilityCooldown>();
    }

    //Ability only instantiate a projectile prefab with data, the projectile contains it's behaviour
    public override void TriggerAbility()
    {
        if (CheckState())
        {
            if(projectileClone != null)
            {
                ProjectileDestroyed();
            }

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
        projectileScript.Speed = targettedSpeed;
        projectileScript.Force = untargettedForce;
        projectileScript.Initiator = Initiator;
        projectileScript.attackElement = attackElement;
    }
}
