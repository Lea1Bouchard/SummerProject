using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "Death", menuName = "UtilityAI/Actions/Death")]
    public class Death : Action
    {
        public override void Execute(EnemyController enemy)
        {
            //enemy.UseAbility();

            Debug.Log("Using Death");
            enemy.OnFinishedAction();
        }
    }
}