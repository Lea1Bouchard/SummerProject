using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace UtilityAI.Core
{
    public class EnemyController : Characters
    {
        public MoveController moveController { get; set; }
        public AIBrain aIBrain { get; set; }
        public Action[] actionsAvailable;

        private EnemyState enemyState;
        public EnemyController()
        {

        }

        private void Start()
        {
            moveController = GetComponent<MoveController>();
            aIBrain = GetComponent<AIBrain>();
        }
    }
}

