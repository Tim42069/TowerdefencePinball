using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingState : BuildState
{
    public static event Action<GameObject> OnMoveTile;
    public static event Action OnPlaceTowerButton;

    public BuildingState(GameStateMachine _stateMachine) : base(_stateMachine) { }
    private float timeEntered;

    public override void Enter()
    {
        base.Enter();
        EventSystem.current.SetSelectedGameObject(null);
        OnMoveTile += SetCurrentTile;

        // Called to update preview sprites after placement
        if (stateMachine.currentTile == null)
        {
            OnMoveTile?.Invoke(MapGen.instance.coordMap[new Vector3Int(0, 0, 0)]);
        }
        timeEntered = 0;
    }
    public override void Exit()
    {
        base.Exit();
        OnMoveTile -= SetCurrentTile;
    }

    public override void Update()
    {
        base.Update();
        timeEntered += Time.deltaTime;
    }

    public override void OnToggleBuild(InputAction.CallbackContext context)
    {
        base.OnToggleBuild(context);
        Debug.Log("FUCKKK");
        stateMachine.ChangeState(stateMachine.buildMenuState);
    }

    public override void OnMoveJoystick(InputAction.CallbackContext context)
    {
        base.OnMoveJoystick(context);
        if (stateMachine.currentTile == null)
        {
            OnMoveTile?.Invoke(MapGen.instance.coordMap[new Vector3Int(0, 0, 0)]);
        }
        // Get direction of point
        Vector2 joystickDirection = context.ReadValue<Vector2>();
        Vector3 hexDirection = GetSnappedDirection(joystickDirection);

        Vector3Int newCoord = MapGen.instance.GetTileCoordinate(stateMachine.currentTile.transform.position + (hexDirection * 1.5f));

        newCoord.z = 0;
        GameObject selectedTile;
        if (!MapGen.instance.coordMap.TryGetValue(newCoord, out selectedTile))
        {
            return;
        }

        // Let everyone know the selected tile has changed
        OnMoveTile?.Invoke(selectedTile);
    }

    public override void OnSelectButton(InputAction.CallbackContext context)
    {
        // Checking time entered to stop placement immedietly after UI interaction
        if (timeEntered < 0.2 && timeEntered > 0.0) return;
        base.OnSelectButton(context);
        GameObject tower;
        if (stateMachine.currentTile != null)
        {
            if (MapGen.instance.GetTowerOnPosition(stateMachine.currentTile.transform.position, out tower))
            {
                stateMachine.ChangeState(stateMachine.upgradeState);
                return;
            }
        }
        OnPlaceTowerButton?.Invoke();
    }

    public static Vector3 GetSnappedDirection(Vector2 stickInput)
    {

        float stickThreshold = 0.1f;

        if (stickInput.magnitude < stickThreshold) return Vector3.zero;

        // Convert joystick input into world direction
        Vector3 worldDir = new Vector3(stickInput.x, 0f, stickInput.y);

        // Convert to map local space
        Vector3 localDir = MapGen.instance.transform.InverseTransformDirection(worldDir);

        float angle = Mathf.Atan2(localDir.z, localDir.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;

        float snapped = Mathf.Round(angle / 60f) * 60f;

        float rad = snapped * Mathf.Deg2Rad;
        Vector3 snappedLocal = new Vector3(Mathf.Cos(rad), 0f, Mathf.Sin(rad));

        // Convert back to world space
        Vector3 snappedWorld = MapGen.instance.transform.TransformDirection(snappedLocal);

        return snappedWorld;
    }

    private void SetCurrentTile(GameObject tile)
    {
        stateMachine.currentTile = tile;
    }
}
