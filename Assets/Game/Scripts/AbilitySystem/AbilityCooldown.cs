using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{

    public string abilityButtonAxisName = "Fire1";
    public Image darkMask;
    public Text cooldownTextDisplay;

    [SerializeField] private Ability ability;
    [SerializeField] private GameObject weaponHolder;
    private Image myButtonImage;
    private AudioSource abilitySource;
    private float cooldownDuration;
    private float nextReadyTime;
    private float cooldownTimeLeft;

    void Start()
    {
        Initialize(ability, weaponHolder);  
    }

    public void Initialize (Ability selectedAbility, GameObject weaponHolder)
    {
        ability = selectedAbility;
        myButtonImage = GetComponent<Image>();
        abilitySource = GetComponent<AudioSource>();
        myButtonImage.sprite = ability.abilityUiSprite;
        darkMask.sprite = ability.abilityUiSprite;
        cooldownDuration = ability.abilityCooldown;
        ability.Initialize();

        AbilityReady();
    }

    IEnumerator CooldownTrigger()
    {
        Cooldown();
        bool cooldownComplete = Time.time > nextReadyTime;
        if (cooldownComplete)
        {
            AbilityReady();
        }
        yield return new WaitForSeconds(0.5f);
    }

    private void AbilityReady()
    {
        cooldownTextDisplay.enabled = false;
        darkMask.enabled = false;
    }

    private void Cooldown ()
    {
        cooldownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(cooldownTimeLeft);
        cooldownTextDisplay.text = roundedCd.ToString();
        darkMask.fillAmount = (cooldownTimeLeft / cooldownDuration);
        StartCoroutine(CooldownTrigger());
    }

    private void ButtonTriggered()
    {
        nextReadyTime = cooldownDuration + Time.time;
        cooldownTimeLeft = cooldownDuration;
        darkMask.enabled = true;
        cooldownTextDisplay.enabled = true;

        abilitySource.clip = ability.abilitySound;
        abilitySource.Play();
        ability.TriggerAbility();
        StartCoroutine(CooldownTrigger());
    }
}
