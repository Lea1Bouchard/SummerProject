using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace UtilityAI.Core
{
    public class EnemyController : Characters
    {
        [Header("Enemy's Stats")]
        public float maxRange;

        [Header("Enemy's States")]
        [SerializeField] private EnemyType enemyType;
        public EnemyState enemyState;
        [HideInInspector] public bool isInFight;
        private Player player;

        [Header("AI and Actions")]
        public List<Action> fightingActionsAvailable;
        public List<Action> normalActionsAvailable;
        public List<Ability> meleesAbilities;
        public Ability rangeAbility;
        public AIBrain aIBrain { get; set; }

        public EnemyController()
        {
            MaxhealthPoints = 100f;
            CurrenthealthPoints = 100f;

            AffinityResistanceModifier = 0.75f;
            WeaknessModifier = 1.25f;
        }

        private void Start()
        {
            aIBrain = GetComponent<AIBrain>();
            enemyState = EnemyState.Idle;
            isInFight = false;

            player = Player.Instance;

            //Initialize abilities
            for (int i = 0; i < meleesAbilities.Count; i++)
            {
                meleesAbilities[i] = Instantiate(meleesAbilities[i]);

                meleesAbilities[i].Initialize(this);
            }

            rangeAbility = Instantiate(rangeAbility);

            rangeAbility.Initialize(this);
        }

        private void Update()
        {
            if (aIBrain.finishedDeciding)
            {
                aIBrain.finishedDeciding = false;
                aIBrain.bestAction.Execute(this);
            }
        }

        public void TriggerInFight()
        {
            enemyState = EnemyState.Attacking;
            isInFight = true;
            GameManager.Instance.AddEnemyToFight(this);
            target = Player.Instance;
        }

        //Called at the end of the animation
        public void OnFinishedAction()
        {
            if (GameManager.Instance.currentGameState == GameState.InFight && isInFight)
            {
                aIBrain.DecideBestAction(fightingActionsAvailable);
            }
            else
            {
                aIBrain.DecideBestAction(normalActionsAvailable);
            }
            
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

