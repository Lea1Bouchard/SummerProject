using Enums;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool ranged;
    public bool dodge;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    [Header("Elements Changes")]
    //Wait Mode
    private InputAction waitModeAction;
    //Elements
    private InputAction elementNormalAction;
    private InputAction elementShiftAction;
    private enum DpadDirection { UP, DOWN, LEFT, RIGHT, NONE }
    [SerializeField] private List<Elements> elementList;

#if ENABLE_INPUT_SYSTEM
    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        MoveInput(inputMovement);
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.ReadValue<Vector2>());
        }
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        JumpInput(value.performed);
    }

    public void OnSprint(InputAction.CallbackContext value)
    {
        SprintInput(value.performed);
    }

    public void OnRangedAbility(InputAction.CallbackContext value)
    {
        if (value.performed)
            Player.Instance.RangedAbility();
    }

    public void OnDodge(InputAction.CallbackContext value)
    {
        if (value.performed)
            Player.Instance.DodgeAbility();
    }

    public void OnMelee(InputAction.CallbackContext value)
    {
        if (value.performed)
            Player.Instance.DodgeAbility();
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
        if (newElement != Elements.Null)
        {
            Player.Instance.ChangeWeaponElement(newElement);
        }
    }

#endif


    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
