using UnityEngine;
using UnityEngine.ProBuilder;

[RequireComponent(typeof(SphereCollider))]
public class OyliskTower : MonoBehaviour
{
    [SerializeField] private float shootInterval;
    [SerializeField] private GameObject closeGoblin;
    [SerializeField] private float radius;
    [SerializeField] private int objectdamage = 1;

    [SerializeField] private float checkRadius = 5f;
    [SerializeField] private LayerMask enemyLayer;


    public Transform lightSpawn;
    public GameObject lightBolt;

    private GameObject lastBolt1;
    private GameObject lastBolt2;


    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip attackClip1;
    [SerializeField] private AudioClip attackClip2;

    [SerializeField] private float time;
    private SphereCollider sphereCollider;
    // Add this field at the top of the class with other fields
    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        time = 0;
        checkRadius = radius * 10;
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
            Debug.DrawLine(transform.position, closeGoblin.transform.position, Color.red, 0.8f);
            goblinHealth goblindamge = closeGoblin.gameObject.GetComponent<goblinHealth>();
            goblindamge.damagetaken(objectdamage);

            //bolt spawn
            GameObject bolt1 = Instantiate(lightBolt);
            Vector3 start = lightSpawn.position;
            Vector3 end = closeGoblin.transform.position;
            Vector3 direction = end - start;
            float targetLength = direction.magnitude;
            // get bolt length
            Mesh mesh = bolt1.GetComponent<MeshFilter>().mesh;
            float meshLength = mesh.bounds.size.z;
            // scale factor needed
            float scaleZ = targetLength / meshLength;
            // apply transform
            bolt1.transform.position = start + direction * 0.5f;
            bolt1.transform.rotation = Quaternion.LookRotation(direction);
            bolt1.transform.localScale = new Vector3(
                bolt1.transform.localScale.x,
                bolt1.transform.localScale.y,
                scaleZ
            );
            lastBolt1 = bolt1;

            AudioSource.PlayOneShot(attackClip1);
            CheckForTargets();
            Debug.Log("Obylisk Tower attacked " + closeGoblin.name);
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
    void CheckForTargets()
    {
        // check for next closet
        Collider[] hits = Physics.OverlapSphere(closeGoblin.transform.position, checkRadius, enemyLayer);

        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            if (hit.gameObject == closeGoblin)
                continue;

            float distance = (hit.transform.position - transform.position).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = hit.transform;
            }
        }

        //attack the closest to the furst goblin hit
        if (closestTarget != null)
        {
            Debug.DrawLine(closeGoblin.transform.position, closestTarget.position, Color.blue, 0.8f);

            goblinHealth goblindamge = closestTarget.GetComponent<goblinHealth>();
            goblindamge.damagetaken(objectdamage);

            AudioSource.PlayOneShot(attackClip2);

            // bolt spawn
            GameObject bolt2 = Instantiate(lightBolt);
            Vector3 start = closeGoblin.transform.position;
            Vector3 end = closestTarget.position;
            Vector3 direction = end - start;

            float targetLength = direction.magnitude;
            float meshLength = bolt2.GetComponent<MeshFilter>().mesh.bounds.size.z;
            float scaleZ = targetLength / meshLength;

            bolt2.transform.position = start + direction * 0.5f;
            bolt2.transform.rotation = Quaternion.LookRotation(direction);
            bolt2.transform.localScale = new Vector3(
                bolt2.transform.localScale.x,
                bolt2.transform.localScale.y,
                scaleZ
            );

            lastBolt2 = bolt2;

            Debug.Log("Closest enemy: " + closestTarget.name);
        }
    }

    private void Update()
    {
        if (time > 0)
        {
            time = time - Time.deltaTime;
        }
        if (time < 1.8f)
        {
            Destroy(lastBolt1);
            Destroy(lastBolt2);
        }
    }
}
