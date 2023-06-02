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
        //isPlayerInFront();
        //isPlayerInLineOfSight();

        currentState.UpdateState(this);
    }

    public void ChangeState(EnemyState state)
    {
        state.ExitState(this);
        currentState = state;
        state.EnterState(this);
    }
   
}
