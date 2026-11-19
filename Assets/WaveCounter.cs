using TMPro;
using UnityEngine;

public class WaveCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text display;
    private void OnEnable() 
    {
        WaveState.WaveStart += OnWaveStart;
    }

    private void OnDisable()
    {
        WaveState.WaveStart -= OnWaveStart;
    }

    private void OnWaveStart(int waveNumber)
    {
        if (waveNumber > WaveState.WinRound)
        {
            display.text = string.Format("Wave :  {0} / {1}", waveNumber, "infinite");
            return;
        }
        display.text = string.Format("Wave :  {0} / {1}", waveNumber, WaveState.WinRound);
    }
}
