using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform ballToFollow;
    [SerializeField] private float lookSpeed = 3; //slider instead?
    [SerializeField] private float horizontalCameraOffset = 3;
    [SerializeField] private float verticalCameraOffset = 3;

    void Start()
    {
        LockMouse();
    }

    void Update()
    {
        // Rotate around ball horizontally (Deternibes what is forward)
        transform.RotateAround(ballToFollow.position, Vector3.up, Input.GetAxis("Mouse X"));

        // Look up and down
        transform.RotateAround(ballToFollow.position, -Camera.main.transform.right, Input.GetAxis("Mouse Y"));

        // Keep up with ball and move back so you can see it
        transform.position = ballToFollow.position - transform.forward * horizontalCameraOffset + transform.up * verticalCameraOffset; //Maybe get this offset as a vec3 in inspector

        //TODO: Stop rotating into floor, walls, etc
    }

    public void LockMouse() //TODO: Move this to somewhere else
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResetCamera()
    {
        transform.rotation = Quaternion.identity;
    }
}
