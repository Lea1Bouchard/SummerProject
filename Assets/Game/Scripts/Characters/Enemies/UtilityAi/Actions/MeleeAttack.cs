using UnityEngine;
using UnityEngine.AI;
using UtilityAI.Core;

namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "MeleeAttack", menuName = "UtilityAI/Actions/Melee Attack")]
    public class MeleeAttack : Action
    {
        public override void Execute(EnemyController enemy)
        {
            float distanceToTarget = Vector3.Distance(enemy.target.transform.position, enemy.transform.position);
            if (distanceToTarget > enemy.meleeRange && enemy.navAgent != null)
            {
                GetCloserToPlayer(enemy);
            }
            else
            {
                DecideAttackToDo(enemy);
                enemy.StopMovement();
                enemy.enemyState = Enums.EnemyState.Attacking;
            }
            enemy.OnFinishedAction();
        }

        private void DecideAttackToDo(EnemyController enemy)
        {
            if (enemy.meleesAbilities.Count == 1)
            {
                enemy.UseAbility(enemy.meleesAbilities[0]);
            }
            else
            {
                if (enemy.enemyType == Enums.EnemyType.Scarab)
                {
                    ScarabsAttacks(enemy);
                }
            }
        }

        private void ScarabsAttacks(EnemyController enemy)
        {
            Ability smashAbility = null;
            Ability stabAbility = null;
            foreach (var ability in enemy.meleesAbilities)
            {
                switch (ability.abilityName)
                {
                    case "Stab":
                        stabAbility = ability;
                        break;
                    case "Smash":
                        smashAbility = ability;
                        break;
                }
            }
            if (enemy.sensor.IsInSight(Player.Instance.gameObject))//If player is in front
            {
                enemy.UseAbility(stabAbility);
            }
            else
            {
                enemy.UseAbility(smashAbility);
                enemy.transform.LookAt(enemy.target.transform);
            }

        }

        private void GetCloserToPlayer(EnemyController enemy)
        {
            Vector3 pointNextToPlayer = enemy.target.transform.position + Random.insideUnitSphere * enemy.meleeRange;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(pointNextToPlayer, out hit, 1.0f, NavMesh.AllAreas))
            {
                Debug.Log("hit.position : " + hit.position);
                Debug.Log("navAgent.remainingDistance : " + enemy.navAgent.remainingDistance);
                enemy.navAgent.SetDestination(hit.position);
                enemy.AnimateMovement();
            }
        }
    }
}
