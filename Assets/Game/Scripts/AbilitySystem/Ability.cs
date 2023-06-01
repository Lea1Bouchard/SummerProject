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
    public Sprite abilityUiSprite;
    public string animationStateName;
    public Elements attackElement;

    //TODO : Check how animations will be handled
    protected Animator animator;
    //TODO : Chack if particle system in the best choice
    protected ParticleSystem effect;

    protected bool isActive = false;
    protected AbilityCooldown abilityCooldownClass;

    public int baseDamage;

    protected Characters initiator;

    protected TypeOfAbility abilityType;

    public List<GameState> GameStateBlocked { get => gameStateBlocked; set => gameStateBlocked = value; }
    public List<MovementState> MovementStateBlocked { get => movementStateBlocked; set => movementStateBlocked = value; }
    public List<FightingState> FightingStateBlocked { get => fightingStateBlocked; set => fightingStateBlocked = value; }
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

        foreach (GameState state in gameStateBlocked)
        {
            //Add check to game state


        }

        foreach (MovementState state in movementStateBlocked)
        {
            //Add check to Movement state
        }

        foreach (FightingState state in fightingStateBlocked)
        {
            //Add check to Fighting state
        }

        Debug.Log("Returned true");
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

    protected void animate()
    {
        animator.SetTrigger(animationStateName);
    }

}
