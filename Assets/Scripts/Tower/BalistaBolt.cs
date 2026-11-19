using System;
using UnityEngine;

public class BalistaBolt : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }



 
        if (!other.CompareTag("Tower")  && !other.CompareTag("Goblin"))
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 5f);
        }
 

    }
}
