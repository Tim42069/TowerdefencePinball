using System;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public static event Action OnDeleteTower;
    public void DeleteTower()
    {
        OnDeleteTower?.Invoke();
    }
}
