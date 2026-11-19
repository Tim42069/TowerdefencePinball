using System;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveState : GameState
{
    public static event Action<int> WaveStart;
    public static int _currentWave;
    public static readonly int WinRound = 10;
    public static event Action<int> updatelist;

    public WaveState(GameStateMachine _stateMachine) : base(_stateMachine)
    {
        Initialise();
        updatelist?.Invoke(1);
    }

    private void Initialise()
    {
        _currentWave = 1;
    }
    public override void Enter()
    {
        base.Enter();
        GoblinSpawner.endwave += OnEndWave;
        WaveStart?.Invoke(_currentWave);
    }

    public override void Exit()
    {
        base.Exit();
        _currentWave += 1;
        GoblinSpawner.endwave -= OnEndWave;
        if (_currentWave == WinRound+1)
        {
            stateMachine.ChangeState(stateMachine.winState);
        }
        updatelist?.Invoke(_currentWave);
    }


    private void OnEndWave(bool toggle)
    {
        stateMachine.ChangeState(stateMachine.buildMenuState);
    }
}
