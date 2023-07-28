using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "Random", menuName = "UtilityAI/Considerations/Random")]
    public class Cons_Random : Consideration
    {
        [SerializeField] private int maxRange;
        [SerializeField] private int maxNumAccepted;
        public override float ScoreConsideration(EnemyController enemy)
        {
            int randNum = Random.Range(0, maxRange);
            Debug.Log(randNum);
            if (randNum <= maxNumAccepted)
                score = 1;
            else
                score = 0;
            return score;
        }
    }
}

