using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateMachine : MonoBehaviour
{
    public GameObject currentTile;

    public GameState buildMenuState;
    public GameState buildingState;
    public GameState upgradeState;
    public GameState waveState;
    public GameState winState;
    public GameState menuState;

    public GameState currentState;
    private GameState nextState;

    public GameObject menu;
    private bool menutoggle=false;
    void Start()
    {
        InitialiseStates();
        InitialiseStateMachine(buildMenuState);
    }

    public void ChangeState(GameState newState)
    {
        nextState = newState;
    }

    private void InitialiseStates()
    {
        buildMenuState = new BuildMenuState(this);
        buildingState = new BuildingState(this);
        upgradeState = new UpgradeState(this);
        waveState = new WaveState(this);
        winState = new WinMenuState(this);
        menuState = new MenuState(this);
    }

    public void InitialiseStateMachine(GameState initialState)
    {
        currentState = initialState;
        currentState.Enter();
    }

    public void OnMoveJoystick(InputAction.CallbackContext context)
    {

        currentState.OnMoveJoystick(context);
    }
    public void OnToggleBuild(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            currentState.OnToggleBuild(context);
        }
    }
    
    public void OnSelectButton(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            currentState.OnSelectButton(context);
        }
    }

    public void OnWaveStartButton(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            currentState.OnWaveStartButton(context);
        }
    }

    public void OnDeselect(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (newtowerinfo.newwavetext==false){
            currentState.OnBackButton(context);
            }
        }
    }

    public void OnMenuButton(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            currentState.OnMenuButton(context);
        }
    }

    void Update()
    {
        if (nextState != null)
        {
            currentState.Exit();
            currentState = nextState;
            nextState = null;
            currentState.Enter();
            Debug.Log(currentState);
        }
        currentState.Update();
    }
}
