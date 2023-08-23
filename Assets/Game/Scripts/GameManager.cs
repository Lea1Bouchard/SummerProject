using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.InputSystem;
using System;
using UtilityAI.Core;

public class GameManager : MonoBehaviour
{
    #region variables
    private static GameManager _instance;
    private static GameState gameState;
    public static event Action<GameState> OnGameStateChanged;
    public GameState currentGameState;

    [Header("UIs")]
    [SerializeField] private Canvas waitModeUI;
    public Sprite[] elementImages;


    [Header("Fighting Manager")]
    public List<EnemyController> enemiesInFight;
    #endregion

    //Instatiation of singleton
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Game Manager is NULL");
            return _instance;
        }
    }
    //Initialize variables
    private void Awake()
    {
        _instance = this;
    }
    //Initialize variables on startup and sets the gamestate to default (InGame)
    private void Start()
    {
        UpdateGameState(GameState.InGame);

        //Wait mode

        waitModeUI.gameObject.SetActive(false);
    }
    //Fills a list with all currently aggresive enemies
    //and puts the game in fight mode if it wasn't
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
    //Sets the game state to a new state and calls the appropriate
    //state change function to match with the new game state
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
    //Resets previousl
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
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void HandleInMenu()
    {
        currentGameState = GameState.InMenu;
        Cursor.lockState = CursorLockMode.None;
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