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
            enemy.enemyState = EnemyState.Flying;
            enemy.Animator.SetBool("IsInFlight", true);
            AnimateFlight(enemy);

            enemy.OnFinishedAction();
        }

        private void AnimateFlight(EnemyController enemy)
        {
            Animator animator = enemy.Animator;
            switch (enemy.flyingState)
            {
                case FlyingState.TakingOff:
                    TakeOff(enemy);
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
                    enemy.navAgent.agentTypeID = enemy.GetNavMeshAgentID("Dragon");
                    break;
            }
        }

        private void TakeOff(EnemyController enemy)
        {
            if (enemy.takeOffStartingPosition != -999)
            {
                enemy.StopMovement();
                // Distance moved equals elapsed time times speed..
                float distCovered = (Time.time - enemy.takeOffStartingPosition) * enemy.takeOffSpeed;

                // Fraction of journey completed equals current distance divided by total distance.
                float fractionOfJourney = distCovered / enemy.flyingHeight;

                // Set our position as a fraction of the distance between the markers.
                Vector3 pos = enemy.transform.position;
                Vector3 lerpVector = Vector3.Lerp(pos, new Vector3(pos.x, pos.y + enemy.flyingHeight, pos.y), fractionOfJourney);
                enemy.flightController.transform.localPosition = new Vector3(0, lerpVector.y, 0);

                if (distCovered >= enemy.flyingHeight)
                {
                    enemy.Animator.SetBool("IsTakeOffDone", true);
                    enemy.flyingState = FlyingState.Floating;
                }
            }
            else
            {
                enemy.takeOffStartingPosition = Time.time;
                enemy.aiSensor.height = 10.5f;
                enemy.navAgent.agentTypeID = enemy.GetNavMeshAgentID("Flying");
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