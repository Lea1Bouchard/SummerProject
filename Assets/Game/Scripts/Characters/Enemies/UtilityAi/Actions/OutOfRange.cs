using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "OutOfRange", menuName = "UtilityAI/Actions/Out of Range")]
    public class OutOfRange : Action
    {
        public override void Execute(EnemyController enemy)
        {
            if (enemy.GetDistanceWithPlayer() > enemy.maxRange)
            {
                enemy.enemyState = Enums.EnemyState.Idle;
                enemy.isInFight = false;
                enemy.target = null;
                GameManager.Instance.RemoveEnemyToFight(enemy);
                enemy.OnFinishedAction();
            }
            enemy.OnFinishedAction();
        }
    }
}