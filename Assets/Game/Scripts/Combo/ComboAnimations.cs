using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAnimations : StateMachineBehaviour
{

    [Tooltip("Damage will be applied to the weapon at the begining of this state")]
    [SerializeField] private float weaponDamage;
    [SerializeField] private float allowedMoveSpeed;
    [SerializeField] private bool disableRoot;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.Instance.weapon.damage = weaponDamage;

        Player.Instance.ChangeMoveSpeed(allowedMoveSpeed);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}



    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!disableRoot)
            animator.ApplyBuiltinRootMotion();
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
