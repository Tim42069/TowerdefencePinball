using System;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private float _speed = 10f;



    private void OnEnable()
    {
        FindClosestGoblin();
    }

    private void FindClosestGoblin()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Goblin");
        float closestDist = float.MaxValue;

        _target = null;

        foreach (var enemy in targets)
        {
            float dist = (enemy.transform.position - transform.position).sqrMagnitude;
            if (dist < closestDist)
            {
                closestDist = dist;
                _target = enemy.transform;
            }
        }
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            _target.position,
            _speed * Time.deltaTime
        );

        /* //if (Vector3.Distance(transform.position, _target.position) < 0.1f)
          {
              goblinHealth goblindamge = _target.gameObject.GetComponent<goblinHealth>();
              goblindamge.damagetaken(999999999);
              Destroy(gameObject);
          }
        */


        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Goblin"))
            {
                Destroy(gameObject);
            }
        }
    }
}