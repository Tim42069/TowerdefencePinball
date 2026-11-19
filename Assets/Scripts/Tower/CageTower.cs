using System;
using UnityEngine;


public class CageTower : MonoBehaviour
{
    [SerializeField] private bool isCaged;
    [SerializeField] private float timeRelease;
    [SerializeField] private GameObject goblin;
    [SerializeField] private Material turnedGoblinColour;
    private Collider cageCollider;

    public int turnedgoblindamage = 3;

    [SerializeField] private float time;

    private void Start()
    {
        cageCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goblin") && !isCaged)
        {
            Debug.Log("Goblin entered the cage!");
            isCaged = true;


            //freaze the goblin's position and disable its movement
            goblin = other.gameObject;
            goblin.transform.position = transform.position;
            goblin.GetComponent<Rigidbody>().isKinematic = true;

            goblin.tag = "Friendly"; // Change the tag to "Friendly" to prevent further interactions with other towers


            cageCollider.enabled = false; // Disable the collider to prevent multiple triggers

            

        }



    }

    // Update is called once per frame
    void Update()
    {
        if (isCaged)
        {
            time += Time.deltaTime;
        }

        if (time >= timeRelease)
        {
            isCaged = false;

            if (goblin != null)
            {
                Rigidbody rb = goblin.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                Debug.Log("Goblin released from the cage!");

                goblin.GetComponentInChildren<Renderer>().material = turnedGoblinColour;
                damagetogoblin newDamage =  goblin.AddComponent<damagetogoblin>(); //add the damge script all towers have
                newDamage.objectdamage = turnedgoblindamage; // Set the damage value for the goblin's attack
                                                             //paul mentioned it may be inefeicnt to add the damage script every time the goblin is released
               
            }
            cageCollider.enabled = true; // Re-enable the collider for future captures
            time = 0;
            Debug.Log("time = " + time);
            Debug.Log("collider enabled");
        }

    }
}