using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICube : MonoBehaviour
{
    float health;
    Rigidbody rb;

    Vector3 _origin;
    IManager manager;

    void Start()
    {
        health = 100f;
        rb = GetComponent<Rigidbody>();
        _origin = transform.position;
        manager = FindObjectOfType<IManager>();
    }

    public void Damage(float damage, Vector3 normal)
    {
        health -= damage;
        Debug.LogWarning("Enemy damaged!");

        if (rb.isKinematic && health <= 0)
        {
            rb.isKinematic = false;
            rb.AddForce(-normal * 150f);
            Debug.LogError("Enemy oofed!");
            StartCoroutine(IRespawn());
            manager.Points(10);
        }
    }

    IEnumerator IRespawn()
    {
        yield return new WaitForSeconds(5f);
        rb.isKinematic = true;
        transform.SetPositionAndRotation(_origin, Quaternion.identity);
        health = 100f;
    }
}