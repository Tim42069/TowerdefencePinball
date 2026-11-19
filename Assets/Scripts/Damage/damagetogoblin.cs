using System;
using UnityEngine;

public class damagetogoblin : MonoBehaviour

{
    public int objectdamage;
    public bool UsetrigerInsteadOfCollider;

    private void OnCollisionEnter(Collision other)
    {
        if (UsetrigerInsteadOfCollider)
        {
            return;
        }
        if (other.gameObject.CompareTag("Goblin"))
        {
            goblinHealth goblindamge=other.gameObject.GetComponent<goblinHealth>();
            goblindamge.damagetaken(objectdamage);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!UsetrigerInsteadOfCollider)
        {
            return;
        }
        if (other.gameObject.CompareTag("Goblin"))
        {
            goblinHealth goblindamge = other.gameObject.GetComponent<goblinHealth>();
            goblindamge.damagetaken(objectdamage);
        }
    }
}

