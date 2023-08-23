using System.Collections.Generic;
using System.Collections;
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
        public int enemyRank;
        [HideInInspector] public bool isInFight;
        private Player player;

        [Header("AI and Actions")]
        public List<Action> fightingActionsAvailable;
        public List<Action> normalActionsAvailable;
        public List<Ability> meleesAbilities;
        public Ability rangeAbility;
        public Ability spellAbility;
        public GameObject midRangeAttack;
        public float midRangeCooldown;
        [HideInInspector] public bool isMidRangeAvailable;
        private bool hasFlameAttack;
        public AIBrain aiBrain { get; set; }
        public AISensor aiSensor { get; set; }

        [Header("Movement")]
        [HideInInspector] public NavMeshAgent navAgent;
        private NavMeshHit navHit;       
        private Vector3 currentDestination;
        [SerializeField] private float maxWalkDistance = 10f;
        #endregion

        [Header("Flying")]
        public GameObject flightController;
        public FlyingState flyingState;
        public float flyingHeight = 5f;
        public float takeOffSpeed = 1f;
        [HideInInspector] public float takeOffStartingPosition = -999;

        private void Start()
        {
            MaxhealthPoints = 100f;
            CurrenthealthPoints = MaxhealthPoints;

            AffinityResistanceModifier = 0.75f;
            WeaknessModifier = 1.25f;

            aiBrain = GetComponent<AIBrain>();
            aiSensor = GetComponent<AISensor>();
            enemyState = EnemyState.Idle;
            flyingState = FlyingState.TakingOff;
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

            isMidRangeAvailable = true;
            if (enemyRank == 3) //Is a dragon
                hasFlameAttack = true;
            else
                hasFlameAttack = false;

            if(hasFlameAttack)
                animator.SetBool("CanFlameAttack", true);

            //Initialize movement components
            navAgent = GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                SetNewDestination();
            }

            if (spellAbility)
            {
                spellAbility = Instantiate(spellAbility);

                spellAbility.Initialize(this);

            }
        }
        //AIbrain executes the action it decided was the best
        //once it has choosen it
        private void Update()
        {
            if (aiBrain.finishedDeciding)
            {
                aiBrain.finishedDeciding = false;
                aiBrain.bestAction.Execute(this);
            }
        }
        //Sets the enemy's state to fight and gives it a target
        public void TriggerInFight()
        {
            enemyState = EnemyState.Attacking;
            isInFight = true;
            animator.SetBool("IsInFight", true);
            GameManager.Instance.AddEnemyToFight(this);
            target = Player.Instance;
        }

public void ExitInFight()
        {
            enemyState = EnemyState.Idle;
            isInFight = false;
            animator.SetBool("IsInFight", false);
            GameManager.Instance.RemoveEnemyToFight(this);
            target = null;
        }

        //Called at the end of the animation
        public void OnFinishedAction()
        {
            if (GameManager.Instance.currentGameState == GameState.InFight && isInFight)
            {
                aiBrain.DecideBestAction(fightingActionsAvailable);
            }
            else
            {
                aiBrain.DecideBestAction(normalActionsAvailable);
            }
        }

        public float GetDistanceWithPlayer()
        {
            return Vector3.Distance(transform.position, player.transform.position);
        }

        public void StartAttackCooldown()
        {
            StartCoroutine(StartCooldown());
        }

        private IEnumerator StartCooldown()
        {
            isMidRangeAvailable = false;
            if (hasFlameAttack)
                animator.SetBool("CanFlameAttack", false);
            yield return new WaitForSeconds(midRangeCooldown);
            isMidRangeAvailable = true;
            if (hasFlameAttack)
                animator.SetBool("CanFlameAttack", true);

        }

        public int GetNavMeshAgentID(string name)
        {
            for (int i = 0; i < NavMesh.GetSettingsCount(); i++)
            {
                NavMeshBuildSettings settings = NavMesh.GetSettingsByIndex(index: i);
                if (name == NavMesh.GetSettingsNameFromID(agentTypeID: settings.agentTypeID))
                    return settings.agentTypeID;
            }
            Debug.LogWarning("GetNavMeshAgentID: This name doesn't exist");
            return -1;
        }

        private void OnDestroy()
        {
            EventManager.Instance.QueueEvent(new KillGameEvent(enemyType));
            player.GetComponent<EnemyLockOn>().RemoveCloseEnemies(this);
        }

        #region Movements
        public void SetNewDestination() //For wandering only
        {
            NavMesh.SamplePosition(((Random.insideUnitSphere * maxWalkDistance) + transform.position), out navHit, maxWalkDistance, -1);

            if (currentDestination != navHit.position)
            {
                currentDestination = navHit.position;
                navAgent.SetDestination(currentDestination);
            }
        }

        public void CheckIfAgentReachedDestination() //For wandering only
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
            if (navAgent != null)
            {
                navAgent.isStopped = true;
                Animator.SetBool("Walk", false);
            }
        }
        #endregion //Movements

        #region Animation Events
        private void StartFireBreath()
        {
            midRangeAttack.SetActive(true);
        }

        private void EndFireBreath()
        {
            midRangeAttack.SetActive(false);
            StartAttackCooldown();
        }
        #endregion //Animation Events
    }
}