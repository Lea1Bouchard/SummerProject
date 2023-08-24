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
                enemy.ExitInFight();
                enemy.OnFinishedAction();
            }
            enemy.OnFinishedAction();
        }
    }
}