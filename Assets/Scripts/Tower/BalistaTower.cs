using UnityEngine;
using UnityEngine.ProBuilder;

[ExecuteAlways]
[RequireComponent(typeof(SphereCollider))]
public class BalistaTower : MonoBehaviour
{
    [SerializeField] private float shootInterval;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float powerForward;
    [SerializeField] private GameObject closeGoblin;
    [SerializeField] private float radius;
    private Transform spawn;
    [SerializeField] private float turnSpeed = 90f; // degrees per second



    private float time;
    private SphereCollider sphereCollider;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        spawn = transform.parent.Find("spawn");
        time = 0;

    }

    private void OnValidate()
    {
        // Called when a value is changed in the Inspector (editor only)
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = radius;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Goblin") && closeGoblin == null)
        {
            closeGoblin = other.gameObject;
        }


        if (time <= 0 && closeGoblin != null)
        {           
            Vector3 direction = (closeGoblin.transform.position - spawn.position).normalized;

            Rigidbody rb = Instantiate(projectilePrefab, spawn.position, Quaternion.LookRotation(direction)).GetComponent<Rigidbody>();

            rb.useGravity = false;
            rb.linearVelocity = direction * powerForward;

            time = shootInterval;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Goblin"))
        {
            closeGoblin = null;
        }
    }

    private void Update()
    {
        Mathf.Clamp(transform.parent.rotation.eulerAngles.x, 15, -15);
        Mathf.Clamp(transform.parent.rotation.eulerAngles.z, 15, -15);
        if (time > 0)
        {
            time = time - Time.deltaTime;
        }

        if (closeGoblin != null)
        {
            Vector3 targetPos = closeGoblin.transform.position;
            targetPos.y = transform.parent.position.y;

            Vector3 direction = targetPos - transform.parent.position;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
}
