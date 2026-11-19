using UnityEngine;
using System;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    [SerializeField] private int startingMoney = 100;
    public int CurrentMoney { get; private set; }

    public event Action<int> OnMoneyChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        CurrentMoney = startingMoney;
    }

    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
        Debug.Log($"Money added: {amount}. New total: {CurrentMoney}");
        OnMoneyChanged?.Invoke(CurrentMoney);
    }

    public bool SpendMoney(int amount)
    {
        if (CurrentMoney < amount)
        {
            Debug.Log("Not enough money to spend!");
            return false;
        }

        CurrentMoney -= amount;
        Debug.Log($"Money spent: {amount}. New total: {CurrentMoney}");
        OnMoneyChanged?.Invoke(CurrentMoney);
        return true;
    }
}