using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.Events;

public class Characters : MonoBehaviour
{
    #region variables
    [Header("Character's Stats")]
    [SerializeField] private float maxhealthPoints;
    private float currenthealthPoints;
    [SerializeField] private int level;
    [SerializeField] private List<Elements> affinities;
    [SerializeField] private List<Elements> weaknesses;
    [SerializeField] private float affinityResistanceModifier;
    [SerializeField] private float weaknessModifier;

    [SerializeField] protected Animator animator;
    private bool isDead = false;
    private bool isImmortal = false;

    public Characters target;
    public GameObject teleportTarget;
    public List<MeleeWeapon> weapons;
    public Transform targetLocation;

    [Header("Weapon Nodes")]
    public Transform[] weaponNodes;
    #endregion

    public class GameObjectEvent : UnityEvent<GameObject> { }
    public GameObjectEvent HealthChanged { get; private set; } = new GameObjectEvent();


    /* Methods */
    public void UseAbility(Ability ability)
    {
        ability.TriggerAbility();
    }
    //Take away the HP from the character
    public void ReceiveDamage(Elements elementHit, float damageReceived)
    {
        if (!isDead && !isImmortal)
        {
            float removeHp = DamageTaken(elementHit, damageReceived);
            if (CurrenthealthPoints - removeHp > 0)
                CurrenthealthPoints -= removeHp;
            else
            {
                CurrenthealthPoints = 0;
                Death();
            }

        }
    }
    //Calculate the total damage taken using the elemental affinities
    private float DamageTaken(Elements elementHit, float damageReceived)
    {
        if (affinities.Contains(elementHit))
        {
            return damageReceived * weaknessModifier;
        }
        else if (weaknesses.Contains(elementHit))
        {
            return damageReceived * affinityResistanceModifier;
        }
        else
        {
            return damageReceived;
        }
    }

    private void Death()
    {
        isDead = true;
        Animator.SetTrigger("Death");
        Destroy(this.gameObject, 1.5f);
    }
    //Activates the weapon's hitbox during an attack
    //Called in animation
    public void ActivateWeapon()
    {
        foreach (MeleeWeapon weapon in weapons)
            weapon.Activate();
    }    
    public void ActivateWeapon(int weaponId)
    {
       weapons[weaponId].Activate();
    }
    //Deactivates the weapon's hitbox during an attack
    //Called in animation
    public void DeactivateWeapon()
    {
        foreach (MeleeWeapon weapon in weapons)
            weapon.Deactivate();
    }
    //Called in teleport ability
    public void SetTeleportTarget(GameObject obj)
    {
        teleportTarget = obj;
    }

    public void RenderImmortal(float time)
    {
        StartCoroutine(ImmortalTimer(time));
    }

    IEnumerator ImmortalTimer(float timeImmortal)
    {
        isImmortal = true;

        yield return new WaitForSeconds(timeImmortal);

        isImmortal = false;
    }

    /* Getters / Setters */
    #region getter/setter
    public int Level { get => level; set => level = value; }
    public float WeaknessModifier { get => weaknessModifier; set => weaknessModifier = value; }
    public List<Elements> Affinities { get => affinities; set => affinities = value; }
    public List<Elements> Weaknesses { get => weaknesses; set => weaknesses = value; }
    public float AffinityResistanceModifier { get => affinityResistanceModifier; set => affinityResistanceModifier = value; }
    public float MaxhealthPoints { get => maxhealthPoints; set => maxhealthPoints = value; }
    public float CurrenthealthPoints { get => currenthealthPoints; set { currenthealthPoints = value; HealthChanged?.Invoke(this.gameObject); } }
    public Animator Animator { get => animator; set => animator = value; }
    public bool IsImmortal { get => isImmortal; set => isImmortal = value; }

    #endregion
}
