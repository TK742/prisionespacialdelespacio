using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IRaycast : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText;
    int ammo;
    bool isReloading;

    float cooldown;
    bool isCooling;

    LineRenderer line;
    LayerMask Layers => LayerMask.GetMask("Default", "Enemy", "World");


    private void Start()
    {
        ammo = 10;
        line = FindObjectOfType<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            ammo--;
            ammoText.text = ammo.ToString();

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 100f, Layers) && hit.collider.TryGetComponent(out ICube cube))
            {
                Debug.Log($"Hit {hit.collider.name}");

                float multiplier = 1.0f;
                float damage = Random.Range(10f, 15f);

                cube.Damage(damage * multiplier, hit.normal);

                line.SetPositions(new Vector3[] { transform.position, hit.point });
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(1.5f);
        isReloading = false;
        ammo = 10;
        ammoText.text = ammo.ToString();
    }
}
