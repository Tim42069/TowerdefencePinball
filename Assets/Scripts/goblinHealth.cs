using System;
using UnityEngine;

public class goblinHealth : MonoBehaviour
{
    public int health = 1;
    public int goblinDefence = 1;
    public float damage = 1;

    [SerializeField] private float friendlyDeathTime = 7;

    public JesterTower pullingTower;
    public GameObject deathVFX;

    public void damagetaken(int damagetaken)
    {
        health -= damagetaken;

        if (health <= 0)
        {
            health = 0;

            Die();

            GetComponent<KillReward>()?.GiveReward();

            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (this.transform.position.y < -500)
        {
            health = 0;

            Die();

            GetComponent<KillReward>()?.GiveReward();

            Destroy(gameObject);
        }

        if (CompareTag("Friendly"))
        {
            friendlyDeathTime -= Time.deltaTime;
        }
        if (friendlyDeathTime <= 0 && CompareTag("Friendly"))
        {
            health = 0;
            Die();
            GetComponent<KillReward>()?.GiveReward();
            Destroy(gameObject);
        }
    }
    void Die()
    {
        if (deathVFX != null)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
        }
    }
}