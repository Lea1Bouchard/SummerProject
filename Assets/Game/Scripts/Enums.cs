using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Darkness
    }

    //EnemyTypes
    public enum EnemyType
    {
        Golem,
        Scarab,
        Slime,
        NightmareDragon
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
        Combat
    }

    //FightingState
    public enum FightingState
    {
        Attacking,
        Teleporting,
        Dodging,
        Moving,
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
        Dodging,
        Blocking
    }
}
