using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    EnemyState currentState;
    EnemyIdleState idleState = new EnemyIdleState();
    EnemyCautiousState cautiousState = new EnemyCautiousState();
    EnemyAttackingState attackingState = new EnemyAttackingState();
    EnemyMovingState movingState = new EnemyMovingState();

    public Transform playerPosition;

    private void Start()
    {
        //Get player reference
        playerPosition = Player.Instance.transform;

        //Starting state of the State Machine
        currentState = idleState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        isPlayerInFront();
        isPlayerInLineOfSight();

        currentState.UpdateState(this);
    }

    public void ChangeState(EnemyState state)
    {
        state.ExitState(this);
        currentState = state;
        state.EnterState(this);
    }
    public bool isPlayerInFront()
    {
        Vector3 directionOfPlayer = transform.position - playerPosition.position;
        float angle = Vector3.Angle(transform.forward, directionOfPlayer);

        if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
        {
            return true;
        }

        return false;
    }

    public bool isPlayerInLineOfSight()
    {
        RaycastHit hit;
        Vector3 directionOfPlayer = transform.position - playerPosition.position;
        directionOfPlayer *= -1f;
        directionOfPlayer = directionOfPlayer.normalized;

        int layer_mask = LayerMask.GetMask("Character");

        if (Physics.Raycast(transform.position, directionOfPlayer, out hit, float.PositiveInfinity, layer_mask))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }
}
