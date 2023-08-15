using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "IsFlying", menuName = "UtilityAI/Considerations/Is Flying")]
    public class Cons_IsFlying : Consideration
    {
        public bool isFlyingReturnOne;
        public override float ScoreConsideration(EnemyController enemy)
        {
            if (enemy.enemyState == Enums.EnemyState.Flying)
            {
                if (isFlyingReturnOne)
                    return score = 1;
                else
                    return score = 0;
            }
            else
                return score = 0;
        }
    }
}

