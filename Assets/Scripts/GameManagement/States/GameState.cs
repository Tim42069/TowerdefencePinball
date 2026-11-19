using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameState
{

    protected private GameStateMachine stateMachine;
    protected bool isExitingState;

    public static event Action<bool> setUpgradeUI;
    public static event Action<bool> setBuildUI;
    public static event Action<bool> setMenuUI;
    public static event Action<bool> setContinueUI;

    public GameState(GameStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
        isExitingState = false;
    }

    public virtual void Exit()
    {
        isExitingState = true;
    }

    // Update method called in state machine
    public virtual void Update() { }

    // Input behaviour methods
    public virtual void OnMoveJoystick(InputAction.CallbackContext context) { }
    public virtual void OnToggleBuild(InputAction.CallbackContext context) { }
    public virtual void OnSelectButton(InputAction.CallbackContext context) { }
    public virtual void OnWaveStartButton(InputAction.CallbackContext context) { }
    public virtual void OnBackButton(InputAction.CallbackContext context) { }
    public virtual void OnMenuButton(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            stateMachine.ChangeState(stateMachine.menuState);
        }
    }

    // UI toggle events
    protected virtual void SetUpgradeUI(bool isActive)
    {
        setUpgradeUI?.Invoke(isActive);
    }
    protected virtual void SetBuildUI(bool isActive)
    {
        setBuildUI?.Invoke(isActive);
    }
    protected virtual void SetMenuUI(bool isActive)
    {
        setMenuUI?.Invoke(isActive);
    }
    protected virtual void SetContinueUI(bool isActive)
    {
        setContinueUI?.Invoke(isActive);
    }



}
