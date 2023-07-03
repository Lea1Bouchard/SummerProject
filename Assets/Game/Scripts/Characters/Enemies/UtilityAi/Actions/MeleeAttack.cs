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
            enemy.StopMovement();
            enemy.enemyState = Enums.EnemyState.Attacking;
            DecideAttackToDo(enemy);
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
            Debug.Log("ScarabsAttacks");
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
            if (enemy.GetDistanceWithPlayer() > enemy.meleeRange)
            {
                Debug.Log("Too far");
                Vector3 pointNextToPlayer = enemy.target.transform.position + Random.insideUnitSphere * enemy.meleeRange;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(pointNextToPlayer, out hit, 1.0f, NavMesh.AllAreas))
                {
                    Debug.Log("hit.position : " + hit.position);
                    Debug.Log("navAgent.remainingDistance : " + enemy.navAgent.remainingDistance);
                    enemy.navAgent.SetDestination(hit.position);
                    enemy.AnimateMovement();

                    if (enemy.navAgent.remainingDistance <= enemy.meleeRange)
                    {
                        enemy.StopMovement();
                        enemy.OnFinishedAction();
                    }
                }
            }
            else
            {
                Debug.Log("Attacking");
                if (enemy.sensor.IsInSight(Player.Instance.gameObject))//If player is in front
                {
                    Debug.Log("Using Stab");
                    enemy.UseAbility(stabAbility);
                }
                else
                {
                    Debug.Log("Using Smash");
                    enemy.UseAbility(smashAbility);
                    enemy.transform.LookAt(enemy.target.transform);
                }
            }
        }
    }
}
