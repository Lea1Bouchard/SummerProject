using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/MeleeAbility")]
public class MeleeAbility : Ability
{

    public Collider attackCollider;
    public Ability nextAttack;
    public string animationTrigger;
    private int hashNextAction;


    public override void Initialize()
    {
        hashNextAction = Animator.StringToHash(animationTrigger);
    }

    public override void TriggerAbility()
    {
        if (CheckState())
        {
            isActive = true;
            animate();
        }
    }

    public void nextAction()
    {
        if (nextAttack != null)
        {
            animator.SetTrigger(hashNextAction);
        }
    }

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


}
