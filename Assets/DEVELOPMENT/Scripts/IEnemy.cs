using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IEnemy : MonoBehaviour
{
    float health;
    Rigidbody rb;
    NavMeshAgent agent;
    bool initialized;

    bool idle;
    public bool Idle => idle;

    public void Initialize()
    {
        if (!initialized)
        {
            initialized = true;
            rb = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
        }

        health = 100f;
        agent.enabled = true;
        idle = false;
    }

    public void Damage(float damage, Vector3 normal)
    {
        health -= damage;
        if (rb.isKinematic && health <= 0)
        {
            agent.enabled = false;
            rb.isKinematic = false;
            rb.AddForce(-normal * 150f);
            StartCoroutine(IRespawn());
            IManager.Instance.Points(10);
        }
    }

    IEnumerator IRespawn()
    {
        yield return new WaitForSeconds(5f);
        rb.isKinematic = true;
        transform.LeanMoveLocalY(transform.position.y - 5f, 3f)
            .setEaseInOutQuad()
            .setOnComplete(() =>
            {
                idle = true;
            });
    }
}
