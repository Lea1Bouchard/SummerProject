using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Characters : MonoBehaviour
{
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

    public Characters target;
    public GameObject teleportTarget;
    public List<MeleeWeapon> weapons;
    public Transform targetLocation;

    [Header("Weapon Nodes")]
    public Transform[] weaponNodes;

    /* Methods */
    //Add Ability and Target in function parameter when created
    public void UseAbility(Ability ability)
    {
        ability.TriggerAbility();
    }

    public void ReceiveDamage(Elements elementHit, float damageReceived)
    {
        if (!isDead)
        {
            float removeHp = DamageTaken(elementHit, damageReceived);
            if (currenthealthPoints - removeHp > 0)
                currenthealthPoints -= removeHp;
            else
            {
                currenthealthPoints = 0;
                Death();
            }

        }
    }

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

    public void ActivateWeapon()
    {
        foreach (MeleeWeapon weapon in weapons)
            weapon.Activate();
    }    
    public void ActivateWeapon(int weaponId)
    {
       weapons[weaponId].Activate();
    }

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

    /* Getters / Setters */
    #region getter/setter
    public int Level { get => level; set => level = value; }
    public float WeaknessModifier { get => weaknessModifier; set => weaknessModifier = value; }
    public List<Elements> Affinities { get => affinities; set => affinities = value; }
    public List<Elements> Weaknesses { get => weaknesses; set => weaknesses = value; }
    public float AffinityResistanceModifier { get => affinityResistanceModifier; set => affinityResistanceModifier = value; }
    public float MaxhealthPoints { get => maxhealthPoints; set => maxhealthPoints = value; }
    public float CurrenthealthPoints { get => currenthealthPoints; set => currenthealthPoints = value; }
    public Animator Animator { get => animator; set => animator = value; }

    #endregion
}
