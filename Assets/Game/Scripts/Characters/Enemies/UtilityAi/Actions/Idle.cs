using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "Idle", menuName = "UtilityAI/Actions/Idle")]
    public class Idle : Action
    {
        public override void Execute(EnemyController enemy)
        {
            enemy.StopMovement();
            switch (enemy.enemyRank)
            {
                case > 0 and <= 2:
                    if (enemy.CurrenthealthPoints != enemy.MaxhealthPoints && enemy.GetDistanceWithPlayer() <= enemy.maxRange)
                        enemy.TriggerInFight();
                    break;
                case 3:
                    if (enemy.sensor.IsInSight(Player.Instance.gameObject) ||
                            (enemy.CurrenthealthPoints != enemy.MaxhealthPoints 
                            && enemy.GetDistanceWithPlayer() <= enemy.maxRange))
                        enemy.TriggerInFight();
                    break;
                default:
                    if (enemy.CurrenthealthPoints != enemy.MaxhealthPoints && enemy.GetDistanceWithPlayer() <= enemy.maxRange)
                        enemy.TriggerInFight();
                    break;
            }

            enemy.OnFinishedAction();
        }
    }
}