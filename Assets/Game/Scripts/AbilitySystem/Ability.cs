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
    public float abilityCooldown;
    public Sprite abilityUiSprite;

    //TODO : Check how animations will be handled
    protected Animation abilityAnimation;
    //TODO : Chack if particle system in the best choice
    protected ParticleSystem effect;

    //TODO : Verify the utility of this one
    protected bool isUsable;

    protected bool isActive;

    public int baseDamage;

    protected Characters initiator;

    protected TypeOfAbility abilityType;

    public List<GameState> GameStateBlocked { get => gameStateBlocked; set => gameStateBlocked = value; }
    public List<MovementState> MovementStateBlocked { get => movementStateBlocked; set => movementStateBlocked = value; }
    public List<FightingState> FightingStateBlocked { get => fightingStateBlocked; set => fightingStateBlocked = value; }
    public Characters Initiator { get => initiator; set => initiator = value; }

    public abstract void Initialize();
    public abstract void TriggerAbility();



    // Start is called before the first frame update
    protected bool CheckState()
    {
        if (!isUsable)
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

        return true;
    }

    protected void OnCancel()
    {
        Destroy(this);
    }

    protected void OnEnd()
    {
        isActive = false;
        isUsable = true;
    }

}
