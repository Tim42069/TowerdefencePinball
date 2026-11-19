using UnityEngine;
using System.Collections.Generic;

public class JesterTower : MonoBehaviour
{
    [SerializeField] private float coolDown = 10f;
    [SerializeField] private float radius;

    [SerializeField] private float checkRadius = 5f;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private float forcePower;
    [SerializeField] private ForceMode forceMode;

    [SerializeField] private float _slowValue = 5;
    private float _defaultDrag = 0.001f;

    [SerializeField] private bool isActive;

    private float time;
    private SphereCollider sphereCollider;

    [SerializeField] private List<GameObject> goblinsInRange = new List<GameObject>();

    [SerializeField] private damagetogoblin damageScript;


    [SerializeField] private AudioSource soundPlayer;
    [SerializeField] private AudioClip attackClip1;
    [SerializeField] private AudioClip attackClip2;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        damageScript = GetComponent<damagetogoblin>();
        time = 0;
        isActive = true;
    }

    private void OnValidate()
    {
        sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider != null)
        {
            sphereCollider.radius = radius;
        }
    }

    private void PlayMusicAudio()
    {
        soundPlayer.clip = attackClip1;
        soundPlayer.Play();
    }

    private void PlayHitAudio()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goblin"))
        {
            soundPlayer.Stop();
            PlayHitAudio();
            
            isActive = false;
            damageScript.enabled = false;
            time = coolDown;
            Disable();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        goblinHealth health = other.GetComponent<goblinHealth>();

        if (other.CompareTag("Goblin") && isActive &&
            (health.pullingTower == null || health.pullingTower == this))
        {
            health.pullingTower = this;

            if (!goblinsInRange.Contains(other.gameObject))
            {
                goblinsInRange.Add(other.gameObject);

            }

        }
    }

    

    private void OnTriggerExit(Collider other)
    {
        if (goblinsInRange.Contains(other.gameObject))
        {
            goblinHealth health = other.GetComponent<goblinHealth>();

            if (health.pullingTower == this)
            {
                health.pullingTower = null;
            }

            goblinsInRange.Remove(other.gameObject);

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearDamping = _defaultDrag;
            }
        }
    }

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time = 0;
            isActive = true;
            damageScript.enabled = true;
        }

        // Clean list (nulls or stolen goblins)
        goblinsInRange.RemoveAll(g =>
            g == null ||
            g.GetComponent<goblinHealth>().pullingTower != this
        );

        if (isActive)
        {
            if (goblinsInRange.Count > 0)
            {
                if (!soundPlayer.isPlaying)
                {
                    PlayMusicAudio();
                }
            }
            foreach (GameObject goblin in goblinsInRange)
            {
                if (goblin != null)
                {
                    goblinHealth health = goblin.GetComponent<goblinHealth>();

                    // ONLY affect goblins this tower owns
                    if (health.pullingTower != this) continue;
                    Rigidbody rb = goblin.GetComponent<Rigidbody>();
                    if (rb == null) continue;
                    rb.linearDamping = _slowValue;
                    rb.AddForce(
                        (transform.position - goblin.transform.position).normalized * forcePower,
                        forceMode
                    );

                    Debug.DrawLine(transform.position, goblin.transform.position, Color.green, 0.1f);
                }
            }
        }
    }

    private void Disable()
    {
        foreach (GameObject goblin in goblinsInRange)
        {
            if (goblin != null)
            {
                goblinHealth health = goblin.GetComponent<goblinHealth>();

                if (health.pullingTower == this)
                {
                    health.pullingTower = null;
                }

                Rigidbody rb = goblin.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearDamping = _defaultDrag;
                }

                Debug.DrawLine(transform.position, goblin.transform.position, Color.black, 0.1f);
            }
        }
    }
}