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
            enemy.CheckIfAgentReachedDestination();
            enemy.AnimateMovement();
            Debug.Log("FLYIIIING");
            if (enemy.CurrenthealthPoints != enemy.MaxhealthPoints)
            {
                enemy.TriggerInFight();
            }

            enemy.OnFinishedAction();
        }
    }
}