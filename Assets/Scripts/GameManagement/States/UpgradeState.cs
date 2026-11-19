using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UpgradeState : BuildState
{
    private GameObject tower;
    public UpgradeState(GameStateMachine _stateMachine) : base(_stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        UpgradeSystem.OnDeleteTower += OnDeleteTower;
        SetUpgradeUI(true);
    }

    public override void Exit()
    {
        base.Exit();
        UpgradeSystem.OnDeleteTower -= OnDeleteTower;
        SetUpgradeUI(false);
    }

    public override void OnBackButton(InputAction.CallbackContext context)
    {
        base.OnBackButton(context);
        stateMachine.ChangeState(stateMachine.buildingState);
    }

    public override void OnToggleBuild(InputAction.CallbackContext context)
    {
        base.OnToggleBuild(context);
        stateMachine.ChangeState(stateMachine.buildMenuState);
    }

    private void OnDeleteTower()
    {
        GameObject tower;
        if (!MapGen.instance.GetTowerOnPosition(stateMachine.currentTile.transform.position, out tower)) return;
        if (MapGen.instance.OnTowerRemove(tower))
        { 
            stateMachine.ChangeState(stateMachine.buildingState);
            Debug.Log("Tower Deleted");
        }
    }
}