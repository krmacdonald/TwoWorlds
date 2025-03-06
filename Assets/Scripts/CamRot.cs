using UnityEngine;

public class CamRot : MonoBehaviour
{
    private const float YMin = -50.0f;
    private const float YMax = 50.0f;

    public Transform lookAt;

    public float distance;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float sensivity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentX += Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        currentY -= Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;
        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Vector3 Direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = lookAt.position + rotation * Direction;

        transform.LookAt(lookAt.position);

        lookAt.rotation = Quaternion.Euler(0f, rotation.eulerAngles.y, 0f);
    }
}
