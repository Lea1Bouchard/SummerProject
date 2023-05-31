using UnityEngine;

public class EnemyIdleState : EnemyState
{
    [SerializeField] private float lineOfSightRadius;
    [SerializeField] private float lineOfSightDistance;

    private RaycastHit raycastHit;
    private bool hasEnemyInLineOfSight = false;

    public override void EnterState(EnemyStateManager manager)
    {
        lineOfSightDistance = 15f;
        lineOfSightRadius = 15f;
    }

    public override void UpdateState(EnemyStateManager manager)
    {
        bool detectedSomething = Physics.SphereCast(manager.transform.position, lineOfSightRadius, manager.transform.forward, out raycastHit, lineOfSightDistance);
        Debug.DrawRay(manager.transform.position, manager.transform.forward);
        if (detectedSomething)
        {
            if (raycastHit.transform.GetType().ToString() == "Player")
            {
                hasEnemyInLineOfSight = true;
                Debug.Log("Detected Player");
            }
        }
    }

    public override void ExitState(EnemyStateManager manager)
    {
        throw new System.NotImplementedException();
    }

}
