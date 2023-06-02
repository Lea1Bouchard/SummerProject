using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Characters : MonoBehaviour
{
    private float maxhealthPoints;
    private float currenthealthPoints;
    private int level;
    [SerializeField] private List<Elements> affinities;
    [SerializeField] private List<Elements> weaknesses;
    private float affinityResistanceModifier;
    private float weaknessModifier;

    [SerializeField] private Animator animator;

    /* Methods */
    //Add Ability and Target in function parameter when created
    public void UseAbility()
    {
        //TODO : Create UseAbility method
    }

    public void ReceiveDamage(Elements elementHit, float damageReceived)
    {
        float removeHp = DamageTaken(elementHit, damageReceived);
        currenthealthPoints -= removeHp;
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
