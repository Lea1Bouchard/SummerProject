using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.InputSystem;
using System;
using UtilityAI.Core;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private static GameState gameState;
    public static event Action<GameState> OnGameStateChanged;
    public GameState currentGameState;

    [Header("UIs")]
    [SerializeField] private Canvas waitModeUI;

    [Header("Fighting Manager")]
    public List<EnemyController> enemiesInFight;

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Game Manager is NULL");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.InGame);

        //Wait mode

        waitModeUI.gameObject.SetActive(false);
    }

    public void AddEnemyToFight(EnemyController enemy)
    {
        enemiesInFight.Add(enemy);
        if (enemiesInFight.Count > 0)
            UpdateGameState(GameState.InFight);
    }

    public void RemoveEnemyToFight(EnemyController enemy)
    {
        if (enemiesInFight.Contains(enemy))
            enemiesInFight.Remove(enemy);

        if (enemiesInFight.Count <= 0)
            UpdateGameState(GameState.InGame);
    }

    #region GameStates
    public void UpdateGameState(GameState newState)
    {
        EndState(gameState);
        gameState = newState;

        switch (newState)
        {
            case GameState.InMenu:
                HandleInMenu();
                break;
            case GameState.InGame:
                HandleInGame();
                break;
            case GameState.InWaitMode:
                HandleInWaitMode();
                break;
            case GameState.InFight:
                HandleInFight();
                break;
            default:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void EndState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.InMenu:
                Time.timeScale = 1;
                break;
            case GameState.InGame:

                break;
            case GameState.InWaitMode:
                Time.timeScale = 1;
                waitModeUI.gameObject.SetActive(false);
                break;
            case GameState.InFight:
                break;
        }
    }

    private void HandleInFight()
    {
        currentGameState = GameState.InFight;
    }

    private void HandleInWaitMode()
    {
        currentGameState = GameState.InWaitMode;
        Time.timeScale = 0;
        waitModeUI.gameObject.SetActive(true);
    }

    private void HandleInGame()
    {
        currentGameState = GameState.InGame;
    }

    private void HandleInMenu()
    {
        currentGameState = GameState.InMenu;
        Time.timeScale = 0;
    }

    public void InWaitMode(InputAction.CallbackContext context)
    {
        if (currentGameState != GameState.InWaitMode)//Enter Wait Mode
        {
            UpdateGameState(GameState.InWaitMode);
        }
        else
        {
            UpdateGameState(GameState.InGame);
        }
    }
    #endregion
}