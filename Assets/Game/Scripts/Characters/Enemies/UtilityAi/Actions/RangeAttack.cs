using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "RangeAttack", menuName = "UtilityAI/Actions/Range Attack")]
    public class RangeAttack : Action
    {
        public override void Execute(EnemyController enemy)
        {
            //enemy.UseAbility();

            Debug.Log("Using Ranged Attack");
            enemy.OnFinishedAction();
            enemy.Animator.SetTrigger("RangeAttack");
        }
    }
}