using UnityEngine;

public abstract class EnemyState
{
    public abstract void EnterState(EnemyStateManager manager);

    public abstract void UpdateState(EnemyStateManager manager);

    public abstract void ExitState(EnemyStateManager manager);
}
