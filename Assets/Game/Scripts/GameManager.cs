using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [Header("Inputs")]
    public InputActionAsset inputActions;
    private InputAction waitModeAction;

    [Header("UIs")]
    [SerializeField] private Canvas waitModeUI;

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
        waitModeAction = inputActions.FindActionMap("Player").FindAction("WaitMode");
    }

    public void InWaitMode(InputAction.CallbackContext context)
    {
        Debug.Log("InWaitMode");
    }

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }
}
