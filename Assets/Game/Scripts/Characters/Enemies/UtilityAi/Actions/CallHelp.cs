using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "CallHelp", menuName = "UtilityAI/Actions/Call Help")]
    public class CallHelp : Action
    {
        public override void Execute(EnemyController enemy)
        {
            //enemy.UseAbility();
            enemy.fightingActionsAvailable.Remove(this);
            enemy.UseAbility(enemy.spellAbility);
            Debug.Log("Calling for help");
        }
    }
}
