using UnityEngine;

public class WaveReward : MonoBehaviour
{
    [SerializeField] private int baseWaveReward = 50;
    [SerializeField] private int rewardPerWaveIncrease = 10;

    private int _currentWave = 0;

    public void OnWaveCompleted()
    {
        _currentWave++;
        int reward = baseWaveReward + (_currentWave - 1) * rewardPerWaveIncrease;

        if (GameEconomy.Instance != null)
        {
            GameEconomy.Instance.AddMoney(reward);
        }
    }
}