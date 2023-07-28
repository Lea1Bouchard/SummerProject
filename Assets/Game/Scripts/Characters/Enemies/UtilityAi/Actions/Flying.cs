using UnityEngine;
using UnityEngine.AI;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "Flying", menuName = "UtilityAI/Actions/Flying")]
    public class Flying : Action
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