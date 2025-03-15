using System.Diagnostics;
using UnityEngine;

public class MirrorManager : MonoBehaviour
{
    public Transform playerCam, mirrorCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posY = new Vector3(transform.position.x, playerCam.transform.position.y, transform.position.z);
        Vector3 side1 = playerCam.transform.position - posY;
        Vector3 side2 = transform.forward;
        float angle = Vector3.SignedAngle(side1, side2, Vector3.up);

        mirrorCam.localEulerAngles = new Vector3(0, angle, 0);
    }
}
