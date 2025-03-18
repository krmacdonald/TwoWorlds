using System.Diagnostics;
using UnityEngine;

// Script by Marcus Ferreira
// A manager intended to allow a surface to accurately act as a mirror by utilizing a camera (moved around a pivot object)
// which renders onto a Render Texture. Intentionally disregards the player camera's Y axis position.

public class MirrorManager : MonoBehaviour
{

    [Tooltip("Place the camera that the mirror will be viewed by in here; assumed to be the player camera.")]
    public Transform playerCamera;

    [Tooltip("Place the pivot point, with the mirror's camera as a child, in here.")]
    public Transform mirrorCamera;

    // Update is called once per frame
    void Update()
    {
        // Uses the player camera's position to calculate the signed angle from the player camera to the pivot object at the front of the mirror
        // Note that the player camera's Y position is subtracted out as to not factor it into the angle's calculation
        Vector3 posY = new Vector3(transform.position.x, playerCamera.transform.position.y, transform.position.z);
        Vector3 side1 = playerCamera.transform.position - posY;
        Vector3 side2 = transform.forward;
        float angle = Vector3.SignedAngle(side1, side2, Vector3.up);

        // Angles the mirror's camera every frame to align with the player camera's position
        mirrorCamera.localEulerAngles = new Vector3(0, angle, 0);
    }
}
