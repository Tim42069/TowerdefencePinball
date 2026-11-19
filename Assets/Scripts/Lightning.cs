using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float timeToDestroy = 0.5f;

    void Update()
    {
        timeToDestroy -= Time.deltaTime;

        if (timeToDestroy <= 0f)
        {
            Destroy(gameObject);
        }
    }
}