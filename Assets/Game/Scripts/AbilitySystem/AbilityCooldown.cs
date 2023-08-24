using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Used by all abilities to stop them from being overused
public class AbilityCooldown : MonoBehaviour
{
    #region variables
    private float cooldownDuration;
    private Ability ability;
    #endregion

    public void Initialize(Ability selectedAbility)
    {
        ability = selectedAbility;
        cooldownDuration = ability.abilityCooldownTime;
        StartCoroutine(CooldownTrigger());
    }

    IEnumerator CooldownTrigger()
    {
        yield return new WaitForSeconds(cooldownDuration * 1f);

        ability.OnEnd();
    }
}
