using System;
using UnityEngine;

public class ShootTower : MonoBehaviour
{
    [SerializeField] private float shootInterval;
    [SerializeField] private GameObject projectilePrefab;
    private float time;

    private void OnEnable()
    {
        time = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (time <= 0 && other.CompareTag("Goblin"))
        {
            Instantiate(projectilePrefab, this.transform.position, this.transform.rotation);
            time = shootInterval;
        }
    }

    private void Update()
    {
        if (time > 0)
        {
            time = time - Time.deltaTime;
        }
    }
}
