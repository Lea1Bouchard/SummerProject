using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/MeleeAbility")]
public class MeleeAbility : Ability
{
    #region variables
    private MeleeWeapon weapon;
    #endregion

    //Parent abstract class implementation

    public override void Initialize(Characters ini)
    {
        animator = ini.GetComponent<Animator>();
        initiator = ini;
        LoadWeaponAttribute();
        abilityType = Enums.TypeOfAbility.Melee;
        abilityCooldownClass = initiator.gameObject.AddComponent<AbilityCooldown>();
    }

    //Parent abstract class implementation
    public override void TriggerAbility()
    {
        if (CheckState())
        {
            isActive = true;
            Animate();
            abilityCooldownClass.Initialize(this);
        }
    }

    //Sets the weapon's stats to the current ability's stats (allows variable damage and element)
    private void LoadWeaponAttribute()
    {
        weapon = initiator.weapon;
        weapon.damage = baseDamage;
        weapon.initiator = initiator;
        weapon.attackElement = attackElement;
        weapon.Deactivate();
    }

}
