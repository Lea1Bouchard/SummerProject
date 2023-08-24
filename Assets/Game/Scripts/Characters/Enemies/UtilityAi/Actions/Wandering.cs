using UnityEngine;
using UnityEngine.AI;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "Wandering", menuName = "UtilityAI/Actions/Wandering")]
    public class Wandering : Action
    {
        public override void Execute(EnemyController enemy)
        {
            enemy.CheckIfAgentReachedDestination();
            enemy.AnimateMovement();

            if (enemy.CurrenthealthPoints != enemy.MaxhealthPoints)
            {
                enemy.TriggerInFight();
            }

            enemy.OnFinishedAction();
        }
    }
}