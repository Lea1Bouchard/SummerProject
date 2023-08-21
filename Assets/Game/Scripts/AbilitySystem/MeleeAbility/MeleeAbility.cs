using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/MeleeAbility")]
public class MeleeAbility : Ability
{
    private MeleeWeapon weapon;
    [SerializeField] private int weaponId;

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
            Debug.Log("Triggered");
            isActive = true;
            Animate();
            abilityCooldownClass.Initialize(this);
        }
    }

    private void LoadWeaponAttribute()
    {
        weapon = initiator.weapons[weaponId];
        weapon.damage = baseDamage;
        weapon.Deactivate();
    }
}
