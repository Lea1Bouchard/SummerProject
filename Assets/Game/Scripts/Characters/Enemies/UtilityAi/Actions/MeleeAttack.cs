using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "MeleeAttack", menuName = "UtilityAI/Actions/Melee Attack")]
    public class MeleeAttack : Action
    {
        public override void Execute(EnemyController enemy)
        {
            enemy.UseAbility(ability);
            enemy.OnFinishedAction();

        }
    }
}
