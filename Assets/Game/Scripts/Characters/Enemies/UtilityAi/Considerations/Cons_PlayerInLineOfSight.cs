using UnityEngine;
using UtilityAI.Core;

namespace UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "PlayerLoS", menuName = "UtilityAI/Considerations/Player in Line of Sight")]
    public class Cons_PlayerInLineOfSight : Consideration
    {
        public override float ScoreConsideration(EnemyController enemy)
        {
            if (isPlayerInFront(enemy) && isPlayerInLineOfSight(enemy))
                return score = 1;
            else
                return score = 0;
        }

        public bool isPlayerInFront(EnemyController enemy)
        {
            Player player = Player.Instance;
            Transform playerPosition = player.transform;
            Vector3 directionOfPlayer = enemy.transform.position - playerPosition.position;
            float angle = Vector3.Angle(enemy.transform.forward, directionOfPlayer);

            if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
                return true;

            return false;
        }

        public bool isPlayerInLineOfSight(EnemyController enemy)
        {
            Player player = Player.Instance;
            Transform playerPosition = player.transform;
            RaycastHit hit;
            Vector3 directionOfPlayer = enemy.transform.position - playerPosition.position;
            directionOfPlayer *= -1f;
            directionOfPlayer = directionOfPlayer.normalized;

            Debug.DrawRay(enemy.transform.position, directionOfPlayer * float.PositiveInfinity, Color.green);
            if (Physics.Raycast(enemy.transform.position, directionOfPlayer, out hit, float.PositiveInfinity))
            {
                if (hit.transform.gameObject.tag == "Player")
                    return true;
            }
            return false;
        }

    }
}

