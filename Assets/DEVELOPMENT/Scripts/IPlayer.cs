using UnityEngine;

public class IPlayer : MonoBehaviour
{
    public float speedNormal = 10.0f;
    public float speedFast = 50.0f;
    public float mouseSensitivityX = 5.0f;
    public float mouseSensitivityY = 5.0f;
    float rotY = 0.0f;

    IGun gun;

    private void Start()
    {
        gun = GetComponentInChildren<IGun>();
    }

    void Update()
    {
        float rotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivityX;
        rotY += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        rotY = Mathf.Clamp(rotY, -89.5f, 89.5f);
        transform.localEulerAngles = new Vector3(-rotY, rotX, 0.0f);

        if (Input.GetMouseButtonDown(0))
            gun.Shoot();
    }
}
