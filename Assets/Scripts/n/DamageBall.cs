using UnityEngine;

public class DamageBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.tag == "Goblin")
        {
            Debug.Log("DamageBall collided with " + collision.gameObject.name);
            Destroy(collision.gameObject);
        }
    }
}
