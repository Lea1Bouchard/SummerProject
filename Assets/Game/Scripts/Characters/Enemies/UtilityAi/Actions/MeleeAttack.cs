using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "MeleeAttack", menuName = "UtilityAI/Actions/Melee Attack")]
    public class MeleeAttack : Action
    {
        public override void Execute(EnemyController enemy)
        {
            //enemy.UseAbility();
            //Debug.Log("Using Melee Attack");
            enemy.OnFinishedAction();
            enemy.Animator.SetTrigger("MeleeAttack");
        }

        public override void Initialize(EnemyController enemy)
        {
            ability.Initialize(enemy);
        }
    }
}
