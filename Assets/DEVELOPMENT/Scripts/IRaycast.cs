using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRaycast : MonoBehaviour
{
    LineRenderer line;
    LayerMask layers => LayerMask.GetMask("Default");

    private void Start()
    {
        line = FindObjectOfType<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)
        && Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 10f, layers)
        && hit.collider.TryGetComponent(out ICube cube))
        {
            Debug.Log($"Hit {hit.collider.name}");
            float multiplier = 1.0f;
            float damage = Random.Range(10f, 15f);
            cube.Damage(damage * multiplier, hit.normal);
            line.SetPositions(new Vector3[] { transform.position, hit.point });
        }
    }
}
