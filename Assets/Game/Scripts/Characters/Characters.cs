using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.Events;

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
    public MeleeWeapon weapon;
    public Transform targetLocation;

    [Header("Weapon Nodes")]
    public Transform[] weaponNodes;

    public class GameObjectEvent : UnityEvent<GameObject> { }
    public GameObjectEvent HealthChanged { get; private set; } = new GameObjectEvent();


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
            if (CurrenthealthPoints - removeHp > 0)
                CurrenthealthPoints -= removeHp;
            else
            {
                CurrenthealthPoints = 0;
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
        weapon.Activate();
    }

    public void DeactivateWeapon()
    {
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
    public float CurrenthealthPoints { get => currenthealthPoints; set { currenthealthPoints = value; HealthChanged?.Invoke(this.gameObject); } }
public Animator Animator { get => animator; set => animator = value; }

    #endregion
}
