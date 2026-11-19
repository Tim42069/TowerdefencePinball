using System;
using UnityEngine;

public class BounceTower : MonoBehaviour
{
    [SerializeField] private float bounce;
    [SerializeField] private CapsuleCollider capsCollider;

    private PhysicsMaterial GetBounceMaterial()
    {
        PhysicsMaterial material = new PhysicsMaterial();
        material.bounciness = 1;
        material.bounceCombine = PhysicsMaterialCombine.Maximum;
        return material;
    }
    
    private void Start()
    {
        capsCollider.material = GetBounceMaterial();
    }

    private void OnCollisionStay(Collision other)
    {
        Debug.Log("Bounce!");
        if (other.collider.CompareTag("Goblin"))
        {
            Rigidbody rb = other.collider.GetComponent<Rigidbody>();
            Vector3 direction = (other.collider.transform.position - this.transform.position).normalized;
            rb.AddForce(direction * -1 * bounce);
        }
    }
}
