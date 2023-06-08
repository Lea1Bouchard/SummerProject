using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

        [Header("Movement")]
        public NavMeshAgent navAgent;
        private Vector3 currentDestination;
        private NavMeshHit navHit;
        [SerializeField] private float maxWalkDistance = 10f;


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

            //Initialize movement components
            navAgent = GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                SetNewDestination();
            }
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

        public void SetNewDestination()
        {
            NavMesh.SamplePosition(((Random.insideUnitSphere * maxWalkDistance) + transform.position), out navHit, maxWalkDistance, -1);

            if (currentDestination != navHit.position)
            {
                currentDestination = navHit.position;
                navAgent.SetDestination(currentDestination);
            }
        }

        public void CheckIfAgentReachedDestination()
        {
            if (!navAgent.pathPending)
            {
                if (navAgent.remainingDistance <= navAgent.stoppingDistance)
                {
                    if (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f)
                    {
                        SetNewDestination();
                    }
                }
            }
        }
    }
}

