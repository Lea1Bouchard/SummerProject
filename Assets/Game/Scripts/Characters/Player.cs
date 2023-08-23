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

    private bool isOverrideRoot;
    private float gravity;

    private bool weaponTrown;

    private PlayerInputHandler _input;

    private RaycastHit raycastHit;

    Dictionary<Elements, Elements> opposingElements;

    [SerializeField] private List<Ability> abilities;
    [SerializeField] private MovementAbility teleportAbility;
    [SerializeField] private float groundCheckDistance;

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

        ResetMoveSpeed();

        gravity = GetComponent<StarterAssets.ThirdPersonController>().Gravity;

        InitializeAbilities();
    }

    private void Update()
    {
        //DetectEnemiesInLineOfSight();
    }


    private void InitializeAbilities()
    {
        foreach (Ability ability in abilities)
        {
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

        //Change animator's element for correct combos
        animator.SetInteger("Element", (int)newElement);
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
        //animator.ResetTrigger("MeleeAttack");
        animator.SetTrigger("NextAction");
        //StartCoroutine(ActionReset());
    }

    public void ActionReset()
    {
        animator.ResetTrigger("NextAction");
        animator.ResetTrigger("MeleeAttack");
    }

    public void SetTarget(Characters enemy)
    {
        target = enemy;

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
            if (!currAbility.IsActive)
            {
                currAbility.TriggerAbility();

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

        foreach (MeleeWeapon weapon in weapons)
            weapon.gameObject.SetActive(false);
    }

    private void RetrieveWeapon()
    {
        weaponTrown = false;
        foreach (MeleeWeapon weapon in weapons)
            weapon.gameObject.SetActive(true);

        RangedAbility rAbility = (RangedAbility)abilities.Find((x) => x.abilityType == TypeOfAbility.Ranged);

        rAbility.ProjectileDestroyed();
    }

    public void UseTargetting()
    {
        if (target == null)
        {
            SetTarget(gameObject.GetComponent<EnemyLockOn>().GetTarget());
        }
        else
        {
            target = null;
            gameObject.GetComponent<EnemyLockOn>().Unfocus();
        }
    }

    public void ChangeTarget()
    {
        if (target != null)
            gameObject.GetComponent<EnemyLockOn>().NextTarget();
    }

    IEnumerator SmoothRotation(float duration, Transform target)
    {
        float t = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        targetRotation.x = transform.rotation.x;
        targetRotation.z = transform.rotation.z;

        while (t < duration)
        {
            t += Time.deltaTime;
            float factor = t / duration;

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, factor);

            yield return null;
        }

        print("Finished rotating");
    }

    public void Interact()
    {
        gameObject.GetComponent<PlayerInteract>().Interact();
    }

    #region Animation Events

    private void StartGroundCheck()
    {
        StartCoroutine(GroundDistanceCheck());
    }

    private void EndGroundCheck()
    {
        StopCoroutine(GroundDistanceCheck());
    }

    IEnumerator GroundDistanceCheck()
    {
        while (true)
        {
            if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.down, groundCheckDistance + 1))
            {
                animator.SetTrigger("GroundClose");
                break;
            }
            yield return new WaitForSeconds(0);
        }
    }

    public void ChangeMoveSpeed(float speed)
    {
        gameObject.GetComponent<StarterAssets.ThirdPersonController>().MoveSpeed = speed;
    }
    public void ResetMoveSpeed()
    {
        gameObject.GetComponent<StarterAssets.ThirdPersonController>().MoveSpeed = movementSpeed;
    }

    private void OverrideRoot(float distance)
    {
        animator.applyRootMotion = false;
        isOverrideRoot = true;
        StartCoroutine(SimulateRootMovement(distance));
    }

    private void EndOverrideRoot()
    {
        isOverrideRoot = false;
        animator.applyRootMotion = true;
    }

    IEnumerator SimulateRootMovement(float distance)
    {
        while (isOverrideRoot)
        {
            gameObject.GetComponent<CharacterController>().Move(gameObject.transform.forward * distance * Time.deltaTime);
            yield return new WaitForSeconds(0);
        }

    }

    private void ResetGroundDistance()
    {
        animator.ResetTrigger("GroundClose");
    }

    private void DisableGravity()
    {
        GetComponent<StarterAssets.ThirdPersonController>().Gravity = 0;
        //jumps with a coefivient of 0, resetting the vertical velocity
        GetComponent<StarterAssets.ThirdPersonController>().AnimJump(0);
    }

    private void EnableGravity()
    {
        GetComponent<StarterAssets.ThirdPersonController>().Gravity = gravity;
    }

    #endregion


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
