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
            /*    // navpath: The NavMeshPath
     
    if (navpath.status == NavMeshPathStatus.PathInvalid || navpath.status == NavMeshPathStatus.PathPartial) {
       // Target is unreachable
    }
*/
            if (distanceToTarget > enemy.meleeRange && enemy.navAgent != null)
                GetCloserToPlayer(enemy);
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
                switch (((int)enemy.enemyType))
                {
                    case >= 0 and <= 3:      //Trees
                        break;
                    case 4:                  //Scarabs
                        ScarabsAttacks(enemy);
                        break;
                    case 5:                  //Golems
                        break;
                    case >= 6 and <= 9:      //Dragons
                        DragonsAttacks(enemy);
                        break;
                    default:
                        break;
                }
            }
        }

        private void ScarabsAttacks(EnemyController enemy)
        {
            Ability smashAbility = null;
            Ability stabAbility = null;
            foreach (var ability in enemy.meleesAbilities)
            {
                if (ability.abilityName == "Stab")
                    stabAbility = ability;
                else if (ability.abilityName == "Smash")
                    smashAbility = ability;
            }
            if (enemy.sensor.IsInSight(Player.Instance.gameObject))//If player is in front
                enemy.UseAbility(stabAbility);
            else
            {
                enemy.UseAbility(smashAbility);
                enemy.transform.LookAt(enemy.target.transform);
            }

        }

        private void DragonsAttacks(EnemyController enemy)
        {
            switch (enemy.enemyType)
            {
                case Enums.EnemyType.Dragon_NoWings:
                    DragonNoWingsAttacks(enemy);
                    break;
                case Enums.EnemyType.Dragon_Chunky:
                    DragonChunkyAttacks(enemy);
                    break;
                case Enums.EnemyType.Dragon_Normal:
                    DragonNormalAttacks(enemy);
                    break;
                case Enums.EnemyType.Dragon_Long:
                    DragonLongAttacks(enemy);
                    break;
            }
        }

        private void DragonNoWingsAttacks(EnemyController enemy)
        {
            Ability basicAbility = null;
            Ability clawAbility = null;
            Ability hornAbility = null;
            foreach (var ability in enemy.meleesAbilities)
            {
                if (ability.abilityName == "Basic")
                    basicAbility = ability;
                else if (ability.abilityName == "Claw")
                    clawAbility = ability;
                else if (ability.abilityName == "Horn")
                    hornAbility = ability;
            }
            if (enemy.sensor.IsDirectlyInFront(enemy.target.gameObject))
                enemy.UseAbility(basicAbility);
            else if (enemy.sensor.IsInSight(enemy.target.gameObject))
            {
                enemy.UseAbility(clawAbility);
                enemy.transform.LookAt(enemy.target.transform);
            }//Else if use horn if player getting away and need small dash
        }

        private void DragonChunkyAttacks(EnemyController enemy)
        {
            throw new System.NotImplementedException();
        }

        private void DragonNormalAttacks(EnemyController enemy)
        {
            Ability basicAbility = null;
            Ability clawAbility = null;
            foreach (var ability in enemy.meleesAbilities)
            {
                if (ability.abilityName == "Basic")
                    basicAbility = ability;
                else if (ability.abilityName == "Claw")
                    clawAbility = ability;
            }
            if (enemy.sensor.IsDirectlyInFront(enemy.target.gameObject))
            {
                enemy.UseAbility(basicAbility);
                Debug.Log("Basic");
            }
            else if (enemy.sensor.IsInSight(enemy.target.gameObject))
            {
                enemy.UseAbility(clawAbility);
                Debug.Log("Claw");
                if(enemy.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == clawAbility.animationStateName)
                    enemy.transform.LookAt(enemy.target.transform);
            }
            else
            {
                Debug.Log("LookAt");
                enemy.transform.LookAt(enemy.target.transform);
            }
        }

        private void DragonLongAttacks(EnemyController enemy)
        {
            throw new System.NotImplementedException();
        }


        private void GetCloserToPlayer(EnemyController enemy)
        {
            Vector3 pointNextToPlayer = enemy.target.transform.position + Random.insideUnitSphere * enemy.meleeRange;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(pointNextToPlayer, out hit, 1.0f, NavMesh.AllAreas))
            {
                enemy.navAgent.SetDestination(hit.position);
                enemy.movementTracker.position = hit.position;
                enemy.AnimateMovement();
            }
        }
    }
}