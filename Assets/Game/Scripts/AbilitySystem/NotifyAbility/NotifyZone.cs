using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAI.Core;

public class NotifyZone : MonoBehaviour
{
    private SphereCollider collide;
    public Characters initiator;
    private List<EnemyController> notified = new List<EnemyController>();
    void Start()
    {
        collide = GetComponent<SphereCollider>();
        StartCoroutine(DetectionTime());
    }

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

        Debug.Log(other.gameObject);
    }

    private void NotifyAllies()
    {
        foreach (EnemyController ally in notified)
        {
            ally.TriggerInFight();
        }

        Destroy(this.gameObject);

    }

    IEnumerator DetectionTime()
    {
        yield return new WaitForSeconds(0.5f);

        NotifyAllies();
    }


}
