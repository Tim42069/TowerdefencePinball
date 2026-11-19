using UnityEngine;
using System;

public class GameEconomy : MonoBehaviour
{
    [SerializeField] GoblinSpawner spawner;
    public static GameEconomy Instance { get; private set; }

    [SerializeField] private int startingMoney = 100;
    public int CurrentMoney { get; private set; }

    public static event Action<int> OnMoneyChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        CurrentMoney = startingMoney;
        OnMoneyChanged?.Invoke(CurrentMoney);
    }

    public bool CanAfford(int amount)
    {
        return CurrentMoney >= amount;
    }

    public bool Spend(int amount)
    {
        if (!CanAfford(amount)) return false;

        CurrentMoney -= amount;
        OnMoneyChanged?.Invoke(CurrentMoney);
        return true;
    }

    public void AddMoney(int amount)
    {
        spawner.updategoblincounter();
        if (amount <= 0) return;
        CurrentMoney += amount;
        OnMoneyChanged?.Invoke(CurrentMoney);
    }

    public void selltower(TowerData tower)
    {
        if (tower == null)
        {
            Debug.LogError("TowerData is NULL");
            Debug.Break();
            return;
        }

        CurrentMoney += tower.cost;
        OnMoneyChanged?.Invoke(CurrentMoney);
    }
}