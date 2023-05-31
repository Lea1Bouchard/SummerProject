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

    public override void Initialize()
    {
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

    //TODO : Might need to make weapon script
    private void OnTriggerEnter(Collider other)
    {
        Characters hit = other.gameObject.GetComponent<Characters>();

        if (isActive && hit != initiator)
        {
            //TODO : Change element to match initiator's element
            hit.DamageTaken(Enums.Elements.Air, baseDamage);
        }
    }

    private void LoadWeaponAttribute()
    {
        weapon.damage = baseDamage;
        weapon.initiator = initiator;
        weapon.attackElement = attackElement;
    }

}
