using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    private void OnEnable()
    {
        
        GameEconomy.OnMoneyChanged += HandleMoneyChanged;
        if (GameEconomy.Instance != null)
        {
            HandleMoneyChanged(GameEconomy.Instance.CurrentMoney);
        }
        
        
    }

    private void OnDisable()
    {
        GameEconomy.OnMoneyChanged += HandleMoneyChanged;
    }

    private void HandleMoneyChanged(int newAmount)
    {
        if (moneyText != null)
        {
            moneyText.text = newAmount.ToString();
        }
    }
}