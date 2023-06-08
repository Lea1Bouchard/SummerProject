using UnityEngine;
using UnityEngine.AI;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "Wandering", menuName = "UtilityAI/Actions/Wandering")]
    public class Wandering : Action
    {
        public override void Execute(EnemyController enemy)
        {
            enemy.enemyState = Enums.EnemyState.Moving;

            enemy.CheckIfAgentReachedDestination();
            Animate(enemy);
            enemy.OnFinishedAction();
        }

        void Animate(EnemyController enemy)
        {
            if (enemy.navAgent.velocity.magnitude > 0)
                enemy.Animator.SetBool("Walk", true);
            else
                enemy.Animator.SetBool("Walk", false);

            Vector3 normalizedMovement = enemy.navAgent.desiredVelocity.normalized;

            Vector3 forwardVector = Vector3.Project(normalizedMovement, enemy.transform.forward);

            Vector3 rightVector = Vector3.Project(normalizedMovement, enemy.transform.right);

            // Dot(direction1, direction2) = 1 if they are in the same direction, -1 if they are opposite
            float forwardVelocity = forwardVector.magnitude * Vector3.Dot(forwardVector, enemy.transform.forward);

            float rightVelocity = rightVector.magnitude * Vector3.Dot(rightVector, enemy.transform.right);
            enemy.Animator.SetFloat("Enemy Z", Mathf.InverseLerp(-1f, 1f, forwardVelocity));
            enemy.Animator.SetFloat("Enemy X", Mathf.InverseLerp(-1f, 1f, rightVelocity));
        }
    }
}