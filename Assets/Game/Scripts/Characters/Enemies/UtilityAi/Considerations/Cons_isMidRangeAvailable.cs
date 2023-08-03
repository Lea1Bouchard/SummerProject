using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "isMidRangeAvailable", menuName = "UtilityAI/Considerations/Is Mid Range Available")]
    public class Cons_isMidRangeAvailable : Consideration
    {
        public override float ScoreConsideration(EnemyController enemy)
        {
            if (enemy.isMidRangeAvailable)
                return score = 1;
            else
                return score = 0;
        }
    }
}

