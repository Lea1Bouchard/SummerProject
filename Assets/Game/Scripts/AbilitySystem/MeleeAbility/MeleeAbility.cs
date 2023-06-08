using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/MeleeAbility")]
public class MeleeAbility : Ability
{

    public string animationTrigger;
    private MeleeWeapon weapon;

    public override void Initialize(Characters ini)
    {
        animator = ini.GetComponent<Animator>();
        initiator = ini;
        LoadWeaponAttribute();
        abilityType = Enums.TypeOfAbility.melee;
        abilityCooldownClass = initiator.gameObject.AddComponent<AbilityCooldown>();
    }


    public override void TriggerAbility()
    {
        if (CheckState())
        {
            isActive = true;
            Animate();
            abilityCooldownClass.Initialize(this);
        }
    }

    private void LoadWeaponAttribute()
    {
        weapon = initiator.weapon;
        weapon.damage = baseDamage;
        weapon.initiator = initiator;
        weapon.attackElement = attackElement;
        weapon.Deactivate();
    }

}
