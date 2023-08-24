using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/NotifyAbility")]
public class NotifyAbility : Ability
{
    #region variables
    [Header("Notification Zone")]

    public GameObject NotifyPrefab;
    private GameObject NotifyClone;
    #endregion

    public override void Initialize(Characters ini)
    {
        animator = ini.GetComponent<Animator>();
        initiator = ini;
        abilityCooldownClass = initiator.gameObject.AddComponent<AbilityCooldown>();
    }

    public override void TriggerAbility()
    {
        if (CheckState())
        {
            isActive = true;
            //prefab is an empty object with NotifyZone script
            NotifyClone = Instantiate(NotifyPrefab, initiator.transform);
            Animate();
            abilityCooldownClass.Initialize(this);
        }
    }
}
