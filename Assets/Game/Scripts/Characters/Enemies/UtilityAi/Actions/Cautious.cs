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

            if (enemy.CurrenthealthPoints != enemy.MaxhealthPoints || enemy.GetDistanceWithPlayer() <= enemy.maxRange / 2)
            {
                enemy.TriggerInFight();
            }

            enemy.OnFinishedAction();
        }
    }
}