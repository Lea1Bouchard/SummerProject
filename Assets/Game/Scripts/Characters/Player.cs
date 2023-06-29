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
    [SerializeField] private MovementAbility teleportAbility;

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

        InitializeAbilities();
    }

    private void Update()
    {
        DetectEnemiesInLineOfSight();
    }

    private void InitializeAbilities()
    {
        foreach (Ability ability in abilities)
        {
            Debug.Log("Initializing : " + ability.abilityName);
            ability.Initialize(this);
        }

        teleportAbility.Initialize(this);
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

    public void SetTarget(Characters enemy)
    {
        target = enemy;

        Debug.Log("Enemy targeted : " + target.name);
        //Automaticly deactivate when player has no target
        gameObject.GetComponent<EnemyLockOn>().ActivateLockonCanvas();

        //TODO : Set special sidestep movement to keep an eye on the target
    }

    public void RangedAbility()
    {
        //TODO : Verify if this is the best way to do it

        if (!weaponTrown)
        {
            Ability currAbility = abilities.Find((x) => x.abilityType == TypeOfAbility.Ranged);
            if(!currAbility.IsActive)
            {
                currAbility.TriggerAbility();

                Debug.Log(currAbility.abilityName);

                ThrowWeapon();
            }
        }
        else
        {
            teleportAbility.TriggerAbility();
            RetrieveWeapon();
        }
    }

    public void DodgeAbility()
    {
        //TODO : Verify if this is the best way to do it

        Ability currAbility = abilities.Find((x) => x.abilityType == TypeOfAbility.Movement);
        currAbility.TriggerAbility();

        Debug.Log(currAbility.abilityName);
        Debug.Log("Dodge");
    }

    public void MeleeAbility()
    {
        //TODO : Verify if this is the best way to do it
        if (!weaponTrown)
        {
            if (target != null)
            {
                StartCoroutine(SmoothRotation(0.2f, target.targetLocation));
            }
                
            Ability currAbility = abilities.Find((x) => x.abilityType == TypeOfAbility.Melee);
            currAbility.TriggerAbility();

            Debug.Log(currAbility.abilityName);
        }
        else
        {
            RetrieveWeapon();
        }
    }

    //Called in animation
    public void TeleportToTarget()
    {
        CharacterController controller = gameObject.GetComponent<CharacterController>();
        controller.enabled = false;
        gameObject.transform.position = teleportTarget.transform.position;
        controller.enabled = true;
    }

    private void ThrowWeapon()
    {
        weaponTrown = true;

        weapon.gameObject.SetActive(false);
    }

    private void RetrieveWeapon()
    {
        weaponTrown = false;

        weapon.gameObject.SetActive(true);

        if (teleportTarget)
            Destroy(teleportTarget, .1f);
    }

    public void UseTargetting()
    {
        if (target == null)
        {
            Debug.Log("Target set");
            SetTarget(gameObject.GetComponent<EnemyLockOn>().GetTarget());
        }
        else
        {
            Debug.Log("Target lost");
            target = null;
            gameObject.GetComponent<EnemyLockOn>().Unfocus();
        }
    }

    public void ChangeTarget()
    {
        gameObject.GetComponent<EnemyLockOn>().NextTarget();
    }

    IEnumerator SmoothRotation(float duration, Transform target)
    {
        float t = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        targetRotation.x = transform.rotation.x;
        targetRotation.z = transform.rotation.z;

        Debug.Log("Quaternion rotation : " + targetRotation);

        while(t < duration)
        {
            t += Time.deltaTime;
            float factor = t / duration;

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, factor);

            yield return null;
        }
    }

    public void Interact()
    {
        gameObject.GetComponent<PlayerInteract>().Interact();
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
