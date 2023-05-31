using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    EnemyState currentState;
    EnemyIdleState idleState = new EnemyIdleState();
    EnemyCautiousState cautiousState = new EnemyCautiousState();
    EnemyAttackingState attackingState = new EnemyAttackingState();
    EnemyMovingState movingState = new EnemyMovingState();

    private void Start()
    {
        //Starting state of the State Machine
        currentState = idleState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void ChangeState(EnemyState state)
    {
        state.ExitState(this);
        currentState = state;
        state.EnterState(this);
    }
}
