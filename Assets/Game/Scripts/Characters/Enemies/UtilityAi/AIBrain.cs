using System.Collections.Generic;
using UnityEngine;

namespace UtilityAI.Core
{
    //Class uses action's consideration to return the best current action to take
    public class AIBrain : MonoBehaviour
    {
        #region variables
        public Action bestAction { get; set; }
        private EnemyController enemy;
        public bool finishedDeciding { get; set; }
        public bool isBrainStopped { get; set; }

        void Start()
        {
            enemy = GetComponent<EnemyController>();
        }
        private void Update()
        {
            if (bestAction is null && !isBrainStopped)
            {
                DecideBestAction(enemy.normalActionsAvailable);
            }
        }

        //Loop through all the available actions
        //Give the highest scoring action
        public void DecideBestAction(List<Action> actionsAvailable)
        {
            float highestScore = 0f;
            int bestActionIndex = 0;
            for (int i = 0; i < actionsAvailable.Count; i++)
            {
                float actionScore = ScoreAction(actionsAvailable[i]);
                if (actionScore > highestScore)
                {
                    bestActionIndex = i;
                    highestScore = actionsAvailable[i].score;
                }
                else if (actionScore == highestScore)
                {
                    if (actionsAvailable[i].Importance > actionsAvailable[bestActionIndex].Importance)
                    {
                        bestActionIndex = i;
                        highestScore = actionsAvailable[i].score;
                    }
                }
            }

            bestAction = actionsAvailable[bestActionIndex];
            finishedDeciding = true;
        }

        //Loop through all considerations of the action
        //Score all the considerations
        //"Average" the consideration score ==> overall action score
        public float ScoreAction(Action action)
        {
            float score = 1f;
            for (int i = 0; i < action.considerations.Length; i++)
            {
                float considerationScore = action.considerations[i].ScoreConsideration(enemy);
                score *= considerationScore;

                if (score == 0)
                {
                    action.score = 0;
                    return action.score; //No point computing further
                }
            }

            //Average scheme of overall score
            float originalScore = score;
            float modFactor = 1 - (1 / action.considerations.Length);
            float makeupValue = (1 - originalScore) * modFactor;
            action.score = originalScore + (makeupValue * originalScore);

            return action.score;
        }
    }
}

