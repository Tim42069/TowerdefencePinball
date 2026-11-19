using UnityEngine;

public class KillReward : MonoBehaviour
{
    [SerializeField] private int rewardAmount = 5;

    private bool _rewardGiven = false;
    // call when enemy dies
    public void GiveReward()
    {
        if (_rewardGiven) return;
        _rewardGiven = true;

        if (GameEconomy.Instance != null)
        {
            GameEconomy.Instance.AddMoney(rewardAmount);
            
        }

    }
}