using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/MeleeAbility")]
public class MeleeAbility : Ability
{

    public Collider attackCollider;
    public Ability nextAttack;
    public string animationTrigger;
    public MeleeWeapon weapon;

    public override void Initialize(Characters ini)
    {
        initiator = ini;
        LoadWeaponAttribute();
    }

    public override void TriggerAbility()
    {
        if (CheckState())
        {
            isActive = true;
            weapon.isActive = true;
            animate();
        }
    }

    public void NextAction()
    {
        if (nextAttack != null)
        {
            animator.SetTrigger(animationTrigger);
        }
    }
    //TODO : Verify if this is used
    public void triggerNextAttack()
    {
        nextAttack.TriggerAbility();
    }

    private void LoadWeaponAttribute()
    {
        weapon.damage = baseDamage;
        weapon.initiator = initiator;
        weapon.attackElement = attackElement;
    }

}
