using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Enums;

namespace UtilityAI.Core
{
    public class EnemyController : Characters
    {
        #region variables
        [Header("Enemy's Stats")]
        public float maxRange;
        public float meleeRange;

        [Header("Enemy's States")]
        public EnemyType enemyType;
        public EnemyState enemyState;
        [HideInInspector] public bool isInFight;
        private Player player;

        [Header("AI and Actions")]
        public List<Action> fightingActionsAvailable;
        public List<Action> normalActionsAvailable;
        public List<Ability> meleesAbilities;
        public Ability rangeAbility;
        public Ability spellAbility;
        public AIBrain aIBrain { get; set; }
        public AISensor sensor { get; set; }

        [Header("Movement")]
        public NavMeshAgent navAgent;
        private Vector3 currentDestination;
        private NavMeshHit navHit;
        [SerializeField] private float maxWalkDistance = 10f;
        #endregion

        //Variables initialization
        public EnemyController()
        {
            MaxhealthPoints = 100f;
            CurrenthealthPoints = 100f;

            AffinityResistanceModifier = 0.75f;
            WeaknessModifier = 1.25f;
        }

        //Variables initialization on startup
        private void Start()
        {
            aIBrain = GetComponent<AIBrain>();
            sensor = GetComponent<AISensor>();
            enemyState = EnemyState.Idle;
            isInFight = false;

            meleeRange = 2f;

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
            spellAbility = Instantiate(spellAbility);

            spellAbility.Initialize(this);
        }
        //AIbrain executes the action it decided was the best
        //once it has choosen it
        private void Update()
        {
            if (aIBrain.finishedDeciding)
            {
                aIBrain.finishedDeciding = false;
                aIBrain.bestAction.Execute(this);
            }
        }
        //Sets the enemy's state to fight and gives it a target
        public void TriggerInFight()
        {
            enemyState = EnemyState.Attacking;
            isInFight = true;
            GameManager.Instance.AddEnemyToFight(this);
            target = Player.Instance;
        }

        //Allows the AIbrain to take another decision
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
        //TODO : The If with the velocity could be done directly in the animator
        //Sets the animator variables to allow a smooth movement animation
        public void AnimateMovement()
        {
            enemyState = EnemyState.Moving;
            navAgent.isStopped = false;

            if (navAgent.velocity.magnitude > 0)
                Animator.SetBool("Walk", true);
            else
                Animator.SetBool("Walk", false);

            Vector3 normalizedMovement = navAgent.desiredVelocity.normalized;

            Vector3 forwardVector = Vector3.Project(normalizedMovement, transform.forward);
            Vector3 rightVector = Vector3.Project(normalizedMovement, transform.right);

            // Dot(direction1, direction2) = 1 if they are in the same direction, -1 if they are opposite
            float forwardVelocity = forwardVector.magnitude * Vector3.Dot(forwardVector, transform.forward);
            float rightVelocity = rightVector.magnitude * Vector3.Dot(rightVector, transform.right);

            Animator.SetFloat("Enemy Z", Mathf.InverseLerp(-1f, 1f, forwardVelocity));
            Animator.SetFloat("Enemy X", Mathf.InverseLerp(-1f, 1f, rightVelocity));
        }

        public void StopMovement()
        {
            navAgent.isStopped = true;
            Animator.SetBool("Walk", false);
        }
        //Removes this enemy from the list of close enemies of the player's targetting
        private void OnDestroy()
        {
            player.GetComponent<EnemyLockOn>().RemoveCloseEnemies(this);
        }
    }
}

