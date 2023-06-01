using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityAI.Core
{
    public class AIBrain : MonoBehaviour
    {
        public Action bestAction { get; set; }
        private EnemyController enemy;

        void Start()
        {
            enemy = GetComponent<EnemyController>();
        }

        public void DecideBestAction(Action[] actionsAvailable)
        {

        }

        public void ScoreAction(Action action)
        {

        }
    }
}

