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
        private Player player;

        [SerializeField] public float maxRange { get; set; }

        public EnemyController()
        {

        }

        private void Start()
        {
            moveController = GetComponent<MoveController>();
            aIBrain = GetComponent<AIBrain>();

            player = Player.Instance;
        }

        private void Update()
        {
            if (aIBrain.finishedDeciding)
            {
                aIBrain.finishedDeciding = false;
                aIBrain.bestAction.Execute(this);
            }
        }
        public void OnFinishedAction()
        {
            aIBrain.DecideBestAction(actionsAvailable);
        }

        public float GetDistanceWithPlayer()
        {
            return Vector3.Distance(transform.position, player.transform.position);
        }

        public bool isPlayerInFront()
        {
            Transform playerPosition = player.transform;
            Vector3 directionOfPlayer = transform.position - playerPosition.position;
            float angle = Vector3.Angle(transform.forward, directionOfPlayer);

            if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
            {
                return true;
            }

            return false;
        }

        public bool isPlayerInLineOfSight()
        {
            Transform playerPosition = player.transform;
            RaycastHit hit;
            Vector3 directionOfPlayer = transform.position - playerPosition.position;
            directionOfPlayer *= -1f;
            directionOfPlayer = directionOfPlayer.normalized;

            int layer_mask = LayerMask.GetMask("Character");

            if (Physics.Raycast(transform.position, directionOfPlayer, out hit, float.PositiveInfinity, layer_mask))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    return true;
                }
            }
            return false;
        }
    }
}

