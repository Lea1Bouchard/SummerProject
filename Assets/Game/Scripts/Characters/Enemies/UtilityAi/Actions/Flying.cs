using Enums;
using UnityEngine;
using UnityEngine.AI;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "Flying", menuName = "UtilityAI/Actions/Flying")]
    public class Flying : Action
    {
        public override void Execute(EnemyController enemy)
        {
            enemy.StopMovement();
            enemy.Animator.SetBool("IsInFlight", true);
            AnimateFlight(enemy);

            enemy.OnFinishedAction();
        }

        private void AnimateFlight(EnemyController enemy)
        {
            Debug.Log("enemy.GetDistanceWithPlayer() : " + enemy.GetDistanceWithPlayer());

            Animator animator = enemy.Animator;
            switch (enemy.flyingState)
            {
                case FlyingState.TakingOff:
                    Vector3 flightController = enemy.flightController.transform.localPosition;
                    float flyingHeight = enemy.transform.position.y - flightController.y;
                    if (flyingHeight < 1)
                       enemy.flightController.transform.localPosition = new Vector3(0, flightController.y += 0.01f, 0);
                    else
                        enemy.flyingState = FlyingState.Floating;

                    enemy.aiSensor.height = 10.5f;
                    enemy.navAgent.agentTypeID = enemy.GetNavMeshAgentID("Flying");
                    break;
                case FlyingState.Floating:
                    if (enemy.GetDistanceWithPlayer() >= 25f)
                        enemy.flyingState = FlyingState.Moving;
                    else if(enemy.aiSensor.IsInSight(enemy.target.gameObject))
                        enemy.flyingState = FlyingState.Attacking;
                    break;
                case FlyingState.Moving:
                    animator.SetBool("Fly", true);
                    Moving(enemy);

                    enemy.flyingState = FlyingState.Floating;
                    break;
                case FlyingState.Gliding:
                    animator.SetBool("Glide", true);
                    Moving(enemy);

                    enemy.flyingState = FlyingState.Floating;
                    break;
                case FlyingState.Attacking:
                    animator.SetTrigger("Fire");

                    enemy.flyingState = FlyingState.Floating;
                    break;
                case FlyingState.Landing:
                    animator.SetTrigger("Land");
                    enemy.Animator.SetBool("IsInFlight", false);
                    enemy.flightController.transform.localPosition = new Vector3(0, 0, 0);

                    enemy.aiSensor.height = 4.5f;
                    break;
            }
        }

        private void Moving(EnemyController enemy)
        {
            Debug.Log("Moving");
            Vector3 pointNextToPlayer = enemy.target.transform.position + Random.insideUnitSphere * 25;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(pointNextToPlayer, out hit, 1.0f, NavMesh.AllAreas))
            {
                enemy.navAgent.SetDestination(hit.position);
                enemy.movementTracker.position = hit.position;
                enemy.enemyState = EnemyState.Moving;
                enemy.navAgent.isStopped = false;

                if (enemy.flyingState == FlyingState.Moving)
                    if (Random.Range(0, 5) == 1)
                        enemy.flyingState = FlyingState.Gliding;
                else if (enemy.flyingState == FlyingState.Gliding)
                    if (Random.Range(0, 5) == 1)
                    enemy.flyingState = FlyingState.Moving;
            }
        }
    }
}