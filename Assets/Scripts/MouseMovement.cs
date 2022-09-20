using UnityEngine;

public class MouseMovement : MonoBehaviour
{
#if UNITY_EDITOR_WIN
    public float mouseSensitivity = 100f;

    public Transform playerTransform;
    
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerTransform.Rotate(Vector3.up * mouseX);

        if (Input.GetMouseButton(0))
        {
            CanGrabCube();
        }
    }

    private bool CanGrabCube()
    {
        RaycastHit hit;

        bool hasHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 15f, 1);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

        return hasHit;
    }

#endif
}