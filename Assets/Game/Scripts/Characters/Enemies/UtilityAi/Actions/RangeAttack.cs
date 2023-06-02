using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "RangeAttack", menuName = "UtilityAI/Actions/RangeAttack")]
    public class RangeAttack : Action
    {
        public override void Execute(EnemyController enemy)
        {
            enemy.UseAbility();
        }
    }
}