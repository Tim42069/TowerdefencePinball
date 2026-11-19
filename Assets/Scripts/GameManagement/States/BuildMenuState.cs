using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildMenuState : BuildState
{
    public BuildMenuState(GameStateMachine _stateMachine) : base(_stateMachine) { }
    

    public override void Enter()
    {
        base.Enter();
        SetBuildUI(true);
    }

    public override void Exit()
    {
        base.Exit();
        SetBuildUI(false);
    }

    public override void OnToggleBuild(InputAction.CallbackContext context)
    {
        base.OnToggleBuild(context);
        Debug.Log("FUCKKK2");
        stateMachine.ChangeState(stateMachine.buildingState);
    }

    public override void OnSelectButton(InputAction.CallbackContext context)
    {
        base.OnSelectButton(context);
        stateMachine.ChangeState(stateMachine.buildingState);
    }

    public override void OnBackButton(InputAction.CallbackContext context)
    {
        base.OnBackButton(context);
        stateMachine.ChangeState(stateMachine.buildingState);
    }
}
