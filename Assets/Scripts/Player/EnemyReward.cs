using UnityEngine;

public class EnemyReward : MonoBehaviour
{
    [SerializeField] private int rewardAmount = 10;

    public void GiveReward()
    {
        MoneyManager.Instance.AddMoney(rewardAmount);
    }

    // Example: call this from your enemy health script
    public void OnDeath()
    {
        GiveReward();
        Destroy(gameObject);
    }
}