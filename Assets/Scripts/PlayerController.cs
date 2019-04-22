using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float slowSpeed = 10f;
    [SerializeField]
    private float fastSpeed = 20f;

    [SerializeField]
    private float drag = 3f;
    [SerializeField]
    private float dragWithRope = 0f;

    private float moveSpeed;
    private Rigidbody rb;
    private CameraController cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            moveSpeed = fastSpeed;
        else
            moveSpeed = slowSpeed;

        if (Input.GetMouseButton(0))
            rb.drag = dragWithRope;
        else
            rb.drag = drag; //maybe after using rope, slowly go down to normal drag
    }

    void FixedUpdate()
    {
        float vertInput = Input.GetAxis("Vertical");
        float horInput = Input.GetAxis("Horizontal");


        //Get current orientation of camera to know where pressign W should make ball go
        //Only x & z values only are wanted, so ignore camera looking into ground

        Vector3 flattendForwardLookDir = Camera.main.transform.forward; 
        flattendForwardLookDir.y = 0;
        flattendForwardLookDir.Normalize();

        Vector3 rightMoveDir = Vector3.Cross(Vector3.up, flattendForwardLookDir);

        Vector3 moveDir = (flattendForwardLookDir * vertInput) + (rightMoveDir * horInput);
        moveDir.Normalize();

        rb.AddForce(moveDir * moveSpeed, ForceMode.Force);                                      //https://answers.unity.com/questions/789917/difference-and-uses-of-rigidbody-force-modes.html


        //TODO: better controlls... dont use addforce? more gravity? or change mass? 
    }
}
