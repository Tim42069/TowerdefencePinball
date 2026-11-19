using UnityEngine;
using UnityEngine.InputSystem;

public class WinMenuState : GameState
{
    public WinMenuState(GameStateMachine _stateMachine) : base(_stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        SetContinueUI(true);
    }

    public override void Exit()
    {
        base.Exit();
        SetContinueUI(false);
    }

    public override void OnSelectButton(InputAction.CallbackContext context)
    {
        base.OnSelectButton(context);
        stateMachine.ChangeState(stateMachine.buildingState);
    }
}
