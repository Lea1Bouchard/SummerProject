using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "PlayerDistance", menuName = "UtilityAI/Considerations/Player Distance")]
    public class Cons_PlayerDistance : Consideration
    {
        [SerializeField] private AnimationCurve responseCurve;
        public override float ScoreConsideration(EnemyController enemy)
        {
            score = responseCurve.Evaluate(Mathf.Clamp01(enemy.GetDistanceWithPlayer() / enemy.maxRange));
            return score;
        }
    }
}

