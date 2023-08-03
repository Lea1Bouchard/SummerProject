using UnityEngine;
using UnityEngine.AI;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "MidRangeAttack", menuName = "UtilityAI/Actions/Mid Range Attack")]
    public class MidRangeAttack : Action
    {
        public override void Execute(EnemyController enemy)
        {
            enemy.Animator.SetTrigger("Fire");
            enemy.OnFinishedAction();
        }
    }
}