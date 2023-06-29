using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "PlayerLoS", menuName = "UtilityAI/Considerations/Player in Line of Sight")]
    public class Cons_PlayerInLineOfSight : Consideration
    {
        public override float ScoreConsideration(EnemyController enemy)
        {
            if (enemy.sensor.IsInSight(Player.Instance.gameObject))
                return score = 1;
            else
                return score = 0;
        }
    }
}

