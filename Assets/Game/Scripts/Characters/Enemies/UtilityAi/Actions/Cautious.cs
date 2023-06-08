using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "Cautious", menuName = "UtilityAI/Actions/Cautious")]
    public class Cautious : Action
    {
        public override void Execute(EnemyController enemy)
        {
            enemy.enemyState = Enums.EnemyState.Cautious;
            enemy.Animator.SetBool("Walk", false);
            enemy.navAgent.isStopped = true;
            enemy.transform.LookAt(Player.Instance.transform);

            enemy.OnFinishedAction();
        }
    }
}