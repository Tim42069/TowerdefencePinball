using UnityEngine;
using UnityEngine.InputSystem;

public class MenuState : GameState
{
    public MenuState(GameStateMachine _stateMachine) : base(_stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        SetMenuUI(true);
    }

    public override void Exit()
    {
        base.Exit();
        SetMenuUI(false);
    }

    public override void OnSelectButton(InputAction.CallbackContext context)
    {
        base.OnSelectButton(context);
        stateMachine.ChangeState(stateMachine.buildingState);
    }

    public override void OnMenuButton(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.buildingState);
    }
}