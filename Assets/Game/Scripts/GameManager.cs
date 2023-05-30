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
    //Wait Mode
    private InputAction waitModeAction;
    private bool isInWaitMode;
    //Elements
    private InputAction elementNormalAction;
    private InputAction elementShiftAction;
    private enum DpadDirection { UP, DOWN, LEFT, RIGHT, NONE }
    [SerializeField] private List<Elements> elementList;

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
    }

    private void Start()
    {
        //Wait mode
        waitModeAction = inputActions.FindActionMap("Player").FindAction("WaitMode");
        elementNormalAction = inputActions.FindActionMap("Player").FindAction("SelectElementNormal");
        elementShiftAction = inputActions.FindActionMap("Player").FindAction("SelectElementShift");
        isInWaitMode = false;
        waitModeUI.gameObject.SetActive(false);
    }

    public void InWaitMode(InputAction.CallbackContext context)
    {
        if (!isInWaitMode)//Enter Wait Mode
        {
            isInWaitMode = true;
            waitModeUI.gameObject.SetActive(true);
        }
        else
        {
            isInWaitMode = false;
            waitModeUI.gameObject.SetActive(false);
        }
    }

    public void SelectElementInputNormal(InputAction.CallbackContext context)
    {
        Vector2 vector = context.ReadValue<Vector2>();
        DpadDirection inputDirection;
        if (vector.x == 1)
            inputDirection = DpadDirection.RIGHT;
        else if (vector.x == -1)
            inputDirection = DpadDirection.LEFT;
        else if (vector.y == 1)
            inputDirection = DpadDirection.UP;
        else if (vector.y == -1)
            inputDirection = DpadDirection.DOWN;
        else
            inputDirection = DpadDirection.NONE;
        ChangeElement(false, inputDirection);
    }

    public void SelectElementInputShift(InputAction.CallbackContext context)
    {
        Vector2 vector = context.ReadValue<Vector2>();
        DpadDirection inputDirection;
        if (vector.x == 1)
            inputDirection = DpadDirection.RIGHT;
        else if (vector.x == -1)
            inputDirection = DpadDirection.LEFT;
        else if (vector.y == 1)
            inputDirection = DpadDirection.UP;
        else if (vector.y == -1)
            inputDirection = DpadDirection.DOWN;
        else
            inputDirection = DpadDirection.NONE;
        ChangeElement(true, inputDirection);
    }

    private void ChangeElement(bool isShiftPressed, DpadDirection dpadDirection)
    {
        Elements newElement = Elements.Null;
        if (!isShiftPressed)
        {
            switch (dpadDirection)
            {
                case DpadDirection.UP:
                    newElement = elementList[0];
                    break;
                case DpadDirection.RIGHT:
                    newElement = elementList[1];
                    break;
                case DpadDirection.DOWN:
                    newElement = elementList[2];
                    break;
                case DpadDirection.LEFT:
                    newElement = elementList[3];
                    break;
            }
        }
        else
        {
            switch (dpadDirection)
            {
                case DpadDirection.UP:
                    newElement = elementList[4];
                    break;
                case DpadDirection.RIGHT:
                    newElement = elementList[5];
                    break;
                case DpadDirection.DOWN:
                    newElement = elementList[6];
                    break;
                case DpadDirection.LEFT:
                    newElement = elementList[7];
                    break;
            }
        }
        if(newElement != Elements.Null)
        {
            Player.Instance.ChangeWeaponElement(newElement);
        }
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
