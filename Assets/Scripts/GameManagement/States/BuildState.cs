using UnityEngine;
using UnityEngine.InputSystem;

public class BuildState : GameState
{
    public BuildState(GameStateMachine _stateMachine) : base(_stateMachine) { }

    public override void OnWaveStartButton(InputAction.CallbackContext context)
    {
        base.OnWaveStartButton(context);
        stateMachine.ChangeState(stateMachine.waveState);
    }

    public override void OnMenuButton(InputAction.CallbackContext context)
    {
        base.OnMenuButton(context);
        stateMachine.ChangeState(stateMachine.menuState);
    }
}
