using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private GameObject canvas;
    private int currentHealth;
    private int damage;

    [Header("UI Settings")]
    public TextMeshProUGUI healthValueText;
    
    private void OnEnable()
    {
        DamageDealer.goblinDamage += TakeDamage;
    }
    private void OnDisable()
    {
        DamageDealer.goblinDamage -= TakeDamage;
    }

    private void Start()
    {
        currentHealth = startingHealth;
        UpdateUI();
    }

    private void Update()
    {
        if (healthValueText != null)
        {
            healthValueText.text = currentHealth + "/" + startingHealth;
        }

    }
    public void TakeDamage(float damage1)
    {
        damage=Convert.ToInt32(damage1);
        UpdateUI();
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);
        if (canvas != null)
        {
            canvas.SetActive(true);
            StartCoroutine(Timer(0.1f));
        }
        
        

        Debug.Log("Player took damage: " + damage + " | Current Health: " + currentHealth);

        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    private void Die() // can be edited for a game over screen instead
    {
        currentHealth = startingHealth;
        Debug.Log("Player has died! Reloading scene...");
        SceneManager.LoadScene("game over");
    }

    private void UpdateUI()
    {
        if (healthValueText != null)
        {
            healthValueText.text = currentHealth + "/" + startingHealth;
        }
    }
    IEnumerator Timer(float howlong)
    {
        yield return new WaitForSeconds(howlong);
        canvas.SetActive(false);
    }
}