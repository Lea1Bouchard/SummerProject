//Use those enums by include "using Enums" in file
namespace Enums
{
    //Elements
    public enum Elements
    {
        Fire,
        Water,
        Air,
        Earth,
        Lightning,
        Ice,
        Light,
        Darkness,
        Null
    }

    //EnemyTypes
    public enum EnemyType
    {
        Tree_Leaf,
        Tree_Spike,
        Tree_Mushroom,
        Tree_Coral,
        Scarab,
        Golem,
        Dragon_NoWings,
        Dragon_Chunky,
        Dragon_Normal,
        Dragon_Long
    }

    //GameState
    public enum GameState
    {
        InMenu,
        InGame,
        InWaitMode,
        InFight
    }

    //MovementState
    public enum MovementState
    {
        Idle,
        Walking,
        Running,
        Falling,
        Jumping,
        OnHoverboard
    }

    //TypeOfAbility
    public enum TypeOfAbility
    {
        Movement,
        Melee,
        Ranged

    }

    //FightingState
    public enum FightingState
    {
        Attacking,
        Teleporting,
        Dodging,
        Moving,
        Dead,
        None
    }

    //EnemyState
    public enum EnemyState
    {
        Idle,
        Cautious,
        Attacking,
        Moving,
        Flying,
        Landing,
        Blocking
    }

    public enum FlyingState
    {
        TakingOff,
        Floating,
        Moving,
        Gliding,
        Attacking,
        Landing
    }

    //NPCs type
    public enum NpcType
    {
        QuestGiver,
        Merchant,
        Civilian
    }

    public enum GoalType
    {
        Slay,
        Talk,
        Gather,
        Bring,
        Fetch
    }
}
