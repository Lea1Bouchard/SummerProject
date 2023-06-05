using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    private float cooldownDuration;
    private Ability ability;

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
