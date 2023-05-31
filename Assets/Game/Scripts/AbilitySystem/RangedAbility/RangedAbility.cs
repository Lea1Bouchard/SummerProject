using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/RangedAbility")]
public class RangedAbility : Ability
{
    public float range = 5f;
    public float speed = 1f;
    public GameObject projectile;
    public GameObject projectileClone;
    public Vector3 startPositionOffset;
    private Projectile projectileScript;

    public override void Initialize(Characters ini)
    {
        isActive = false;
        initiator = ini;

        abilityType = Enums.TypeOfAbility.ranged;
    }

    public override void TriggerAbility()
    {
        if (CheckState())
        {
            projectileClone = Instantiate(projectile, initiator.transform.Find("ProjectileSpawn").position, initiator.transform.rotation);
            SetProjectileValues();
            projectileClone.GetComponent<Projectile>().Shoot(this);
            isActive = true;
        }

    }

    public void ProjectileDestroyed()
    {
        Destroy(projectileClone);
        isActive = false;
    }

    private void SetProjectileValues()
    {
        projectileScript = projectileClone.GetComponent<Projectile>();

        projectileScript.Damage = baseDamage;
        projectileScript.Range = range;
        projectileScript.Speed = speed;
        projectileScript.Initiator = Initiator;
        projectileScript.attackElement = attackElement;
        
    }
}
