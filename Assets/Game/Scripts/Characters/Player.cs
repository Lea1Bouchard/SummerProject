using System.Collections.Generic;
using UnityEngine;
using Enums;
using System.Collections;

public class Player : Characters
{
    private static Player _instance;

    private MovementState movementState;
    private FightingState fightingState;

    private float maxStamina;
    private float currentStamina;

    private float xpToLevelUp;
    private float currentXp;

    private float movementSpeed;

    private bool hasEnemyInLineOfSight;
    private float lineOfSightDistance;
    private float lineOfSightRadius;

    private bool weaponTrown;

    private PlayerInputHandler _input;

    private RaycastHit raycastHit;

    Dictionary<Elements, Elements> opposingElements;

    [SerializeField] private List<Ability> abilities;

    public static Player Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Player is NULL");
            return _instance;
        }
    }

    public Player()
    {
        MaxhealthPoints = 100f;
        CurrenthealthPoints = MaxhealthPoints;
        Level = 1;
        AffinityResistanceModifier = 0.75f;
        WeaknessModifier = 1.25f;

        movementState = MovementState.Idle;
        fightingState = FightingState.None;

        maxStamina = 100f;
        currentStamina = maxStamina;

        xpToLevelUp = 100f;
        currentXp = 0f;

        movementSpeed = 2f;

        hasEnemyInLineOfSight = false;
        lineOfSightDistance = 15f;
        lineOfSightRadius = 15f;

        weaponTrown = false;
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        opposingElements = new Dictionary<Elements, Elements>();
        opposingElements.Add(Elements.Fire, Elements.Water);
        opposingElements.Add(Elements.Water, Elements.Fire);

        opposingElements.Add(Elements.Air, Elements.Earth);
        opposingElements.Add(Elements.Earth, Elements.Air);

        opposingElements.Add(Elements.Lightning, Elements.Ice);
        opposingElements.Add(Elements.Ice, Elements.Lightning);

        opposingElements.Add(Elements.Light, Elements.Darkness);
        opposingElements.Add(Elements.Darkness, Elements.Light);

        opposingElements.Add(Elements.Null, Elements.Null);
    }

    private void Update()
    {
        DetectEnemiesInLineOfSight();
    }

    public void ChangeWeaponElement(Elements newElement)
    {
        //Empty Affinities and Weaknesses lists
        Affinities.Clear();
        Weaknesses.Clear();

        //Change to new Affinities and Weaknesses
        Affinities.Add(newElement);
        Weaknesses.Add(opposingElements[newElement]);
    }

    private void DetectEnemiesInLineOfSight()
    {
        bool detectedSomething = Physics.SphereCast(transform.position, lineOfSightRadius, transform.forward, out raycastHit, lineOfSightDistance);
        if (detectedSomething)
        {
            if (raycastHit.transform.GetType().ToString() == "Enemy")
            {
                hasEnemyInLineOfSight = true;
            }
        }
    }

    public void NextAction() // called by animation event
    {
        animator.ResetTrigger("MeleeAttack");
        animator.SetTrigger("NextAction");
        StartCoroutine(ActionReset());
    }

    IEnumerator ActionReset()
    {
        yield return new WaitForSeconds(0.5f);
        animator.ResetTrigger("NextAction");
        animator.ResetTrigger("MeleeAttack");
    }

    public void RangedSpell()
    {
        //TODO : Verify if this is the best way to do it
        abilities.Find((x) => x.abilityType == TypeOfAbility.Ranged).TriggerAbility();
    }

    public void TeleportSpell()
    {
        //TODO : Verify if this is the best way to do it
        abilities.Find((x) => x.abilityType == TypeOfAbility.Movement).TriggerAbility();
    }

    public void MeleeSpell()
    {
        //TODO : Verify if this is the best way to do it
        abilities.Find((x) => x.abilityType == TypeOfAbility.Melee).TriggerAbility();
    }

    /* Getters / Setters */
    #region getter/setter
    public MovementState MovementState { get => movementState; set => movementState = value; }
    public FightingState FightingState { get => fightingState; set => fightingState = value; }
    public float MaxStamina { get => maxStamina; set => maxStamina = value; }
    public float CurrentStamina { get => currentStamina; set => currentStamina = value; }
    public float XpToLevelUp { get => xpToLevelUp; set => xpToLevelUp = value; }
    public float CurrentXp { get => currentXp; set => currentXp = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public bool HasEnemyInLineOfSight { get => hasEnemyInLineOfSight; set => hasEnemyInLineOfSight = value; }
    #endregion
}
