using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "Health", menuName = "UtilityAI/Considerations/Health")]
    public class Cons_Health : Consideration
    {
        [SerializeField] private AnimationCurve responseCurve;
        public override float ScoreConsideration(EnemyController enemy)
        {
            score = responseCurve.Evaluate(Mathf.Clamp01(enemy.CurrenthealthPoints / enemy.MaxhealthPoints));
            return score;
        }
    }
}

