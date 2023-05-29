using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Characters : MonoBehaviour
{
    private int healthPoints;
    private int level;
    private List<Elements> affinities;
    private List<Elements> weaknesses;
    private float affinityResistanceModifier;
    private float weaknessModifier;

    [SerializeField] private Animator animator;

    /* Methods */
    //Add Ability and Target in function parameter when created
    private void UseAbility()
    {
        //TODO : Create UseAbility method
    }

    public float DamageTaken(Elements elementHit, float damageReceived)
    {
        if (affinities.Contains(elementHit))
        {
            return damageReceived * weaknessModifier;
        }
        else if(weaknesses.Contains(elementHit))
        {
            return damageReceived * affinityResistanceModifier;
        }
        else{
            return damageReceived;
        }
    }

    /* Getters / Setters */
    #region getter/setter
    public int HealthPoints { get => healthPoints; set => healthPoints = value; }
    public int Level { get => level; set => level = value; }
    public float WeaknessModifier { get => weaknessModifier; set => weaknessModifier = value; }
    public List<Elements> Affinities { get => affinities; set => affinities = value; }
    public List<Elements> Weaknesses { get => weaknesses; set => weaknesses = value; }
    public float AffinityResistanceModifier { get => affinityResistanceModifier; set => affinityResistanceModifier = value; }

    #endregion
}
