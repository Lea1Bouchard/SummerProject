using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/MeleeAbility")]
public class MeleeAbility : Ability
{
    #region variables
    private MeleeWeapon weapon;
    [SerializeField] private int weaponId;

    #endregion

    //Parent abstract class implementation

    public override void Initialize(Characters ini)
    {
        isActive = false;
        animator = ini.GetComponent<Animator>();
        initiator = ini;
        LoadWeaponAttribute();
        abilityType = Enums.TypeOfAbility.Melee;
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

    //Sets the weapon's stats to the current ability's stats (allows variable damage and element)
    private void LoadWeaponAttribute()
    {
        weapon = initiator.weapons[weaponId];
        weapon.damage = baseDamage;
        weapon.Deactivate();
    }
}
