using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject questMenu;
    [SerializeField] GameObject resumeButton;
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
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.Instance.UpdateGameState(Enums.GameState.InMenu);
        gameObject.GetComponent<Menu>().ChangeButton(resumeButton);
        CurrentObjectiveIndicator.Instance.gameObject.SetActive(false);
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
        gameObject.GetComponent<Menu>().ChangeButton(resumeButton);
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
        questMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
        CurrentObjectiveIndicator.Instance.gameObject.SetActive(true);
        GameManager.Instance.UpdateGameState(Enums.GameState.InGame);
    }

}
