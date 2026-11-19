using UnityEngine;

public class DeleateDeathVFX : MonoBehaviour
{
    ParticleSystem deathVFX;

    void Start()
    {
        deathVFX = GetComponent<ParticleSystem>();
        Destroy(gameObject, deathVFX.main.duration + deathVFX.main.startLifetime.constantMax);
    }
}
