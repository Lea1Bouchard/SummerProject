using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "Idle", menuName = "UtilityAI/Actions/Idle")]
    public class Idle : Action
    {
        public override void Execute(EnemyController enemy)
        {
            if (enemy.CurrenthealthPoints != enemy.MaxhealthPoints)
            {
                enemy.enemyState = Enums.EnemyState.Attacking;
                GameManager.Instance.UpdateGameState(Enums.GameState.InFight);
            }
            enemy.OnFinishedAction();
        }
    }
}