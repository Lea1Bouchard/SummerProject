using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public abstract class Ability : ScriptableObject
{

    private List<GameState> gameStateBlocked;
    private List<MovementState> movementStateBlocked;
    private List<FightingState> fightingStateBlocked;


    public string abilityName;
    public int abilityCost;
    public AudioClip abilitySound;
    public float abilityCooldownTime;
    public string animationStateName;
    protected Elements attackElement;

    //TODO : Check how animations will be handled
    protected Animator animator;
    //TODO : Chack if particle system in the best choice
    protected ParticleSystem effect;

    protected bool isActive = false;
    protected AbilityCooldown abilityCooldownClass;

    public int baseDamage;

    protected Characters initiator;

    [HideInInspector] public TypeOfAbility abilityType;

    public Characters Initiator { get => initiator; set => initiator = value; }

    public abstract void Initialize(Characters ini);
    public abstract void TriggerAbility();

    // Start is called before the first frame update
    protected bool CheckState()
    {
        if (isActive)
        {
            return false;
        }

        if (GameManager.Instance.currentGameState == GameState.InWaitMode)
            GameManager.Instance.UpdateGameState(GameState.InFight);

        return true;
    }

    protected void OnCancel()
    {
        //Destroy(this);
    }

    public void OnEnd()
    {
        isActive = false;
    }

    protected void Animate()
    {
        animator.SetTrigger(animationStateName);
    }

}
