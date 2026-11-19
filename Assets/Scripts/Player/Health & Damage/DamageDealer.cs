using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 1f; // how long after the player takes damage the ball dissapears
    public static event Action<float> goblinDamage;
    public GameObject boombox;
    Vector3 thisposition;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.CompareTag("Goblin"))
        {
            goblinHealth goblin = other.gameObject.GetComponent<goblinHealth>();
            if (goblin != null)
            {
                goblinDamage?.Invoke(goblin.damage);
                try
                {
                    if(other.name== "Goblin Bomb Ball(Clone)")
                    {
                        thisposition = this.transform.position;
                        Instantiate(boombox, thisposition, (this.transform.rotation));
                    }
                }
                catch { Debug.Log("no boom"); }
                // goblin.damagetaken(9999999);
                Destroy(other.gameObject, destroyDelay);
            }
        }
    }
}