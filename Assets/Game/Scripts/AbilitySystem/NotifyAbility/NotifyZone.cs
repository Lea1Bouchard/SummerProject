using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAI.Core;

public class NotifyZone : MonoBehaviour
{
    #region variables
    private SphereCollider collide;
    public Characters initiator;
    private List<EnemyController> notified = new List<EnemyController>();
    #endregion

    void Start()
    {
        collide = GetComponent<SphereCollider>();
        StartCoroutine(DetectionTime());
    }

    //Populates the list to notify
    private void OnTriggerEnter(Collider other)
    {
        EnemyController hit = other.gameObject.GetComponent<EnemyController>();
        if (hit != initiator && hit != null)
        {
            if (!notified.Contains(hit))
            {
                notified.Add(hit);
            }
        }
    }

    //Sets the enemies in the list in fight mode
    private void NotifyAllies()
    {
        foreach (EnemyController ally in notified)
        {
            ally.TriggerInFight();
        }

        Destroy(this.gameObject);

    }
    //Gives a little time for the detection to occur
    IEnumerator DetectionTime()
    {
        yield return new WaitForSeconds(0.5f);

        NotifyAllies();
    }


}
