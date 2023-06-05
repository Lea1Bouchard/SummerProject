using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "Idle", menuName = "UtilityAI/Actions/Idle")]
    public class Idle : Action
    {
        public override void Execute(EnemyController enemy)
        {
            //enemy.UseAbility();

            //Debug.Log("Using Idle");
            enemy.OnFinishedAction();
            enemy.Animator.SetTrigger("Idle");
        }

        public override void Initialize(EnemyController enemy)
        {
        }
    }
}