using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject questMenu;
    private static PauseMenu _instance;

    public static PauseMenu Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("PauseMenu is NULL");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.Instance.UpdateGameState(Enums.GameState.InMenu);
    }

    public void PauseGame()
    {
        gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
    }

    public void QuestMenu()
    {
        pauseMenu.gameObject.SetActive(false);
        questMenu.gameObject.SetActive(true);
    }
    public void ReturnToPauseMenu()
    {
        questMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
    }

    public void OptionMenu()
    {
        //TODO : Open the option menu
    }

    public void QuitGame()
    {
        Application.wantsToQuit += WantsToQuit;
    }

    static bool WantsToQuit()
    {
        //THIS IS WHERE WE DO THE VOODOO QUITTING MAGIC
        Debug.Log("Player trying to quit.");
        return true;
    }

    private void OnDisable()
    {
        GameManager.Instance.UpdateGameState(Enums.GameState.InGame);
    }

}
