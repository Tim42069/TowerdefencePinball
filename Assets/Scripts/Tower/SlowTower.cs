using System;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public class SlowTower : MonoBehaviour
{
    private SphereCollider _slowCollider;
    private float _slowValue = 5;
    private float _defaultDrag = 0.001f;
    private void Start()
    {
        _slowCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Goblin"))
        {
            Debug.Log("Goblin enter");
            other.GetComponent<Rigidbody>().linearDamping = _slowValue;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Goblin"))
        {
            Debug.Log("Goblin Exit");
            other.GetComponent<Rigidbody>().linearDamping = _defaultDrag;
        }
    }
}
