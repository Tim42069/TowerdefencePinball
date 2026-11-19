using UnityEngine;

public class speedball : MonoBehaviour
{
    Rigidbody rb;
    public float motion = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y > -2.5)
        {
            rb.AddForce(-transform.forward * motion);
            float move = Random.Range(3, -3);
            rb.rotation.Set(this.transform.rotation.x + move, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w);
        }
        if (Time.time > 4)
        {
            motion = 0;
        }
        if (Time.time > 5)
        {
            motion = 5;
        }
    }

}
