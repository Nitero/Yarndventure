using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;
    private Rigidbody rb;
    private CameraController cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.GetComponent<CameraController>();
    }


    void FixedUpdate()
    {
        float vertInput = Input.GetAxis("Vertical"); //forward, backwards
        float horInput = Input.GetAxis("Horizontal"); //left, right



        //Get current orientation of camera to know where pressign W should make ball go

        Vector3 flattendForwardLookDir = Camera.main.transform.forward; //x und z werte, zeigen aber leicht in boden, wie kamera... daher normalisieren um nur auf boden entlang zu bewegen
        flattendForwardLookDir.y = 0;
        flattendForwardLookDir.Normalize();
        //print(flattendForwardLookDir);

        Vector3 rightMoveDir = Vector3.Cross(Vector3.up, flattendForwardLookDir);

        Vector3 moveDir = (flattendForwardLookDir * vertInput) + (rightMoveDir * horInput);
        moveDir.Normalize();

        rb.AddForce(moveDir * moveSpeed, ForceMode.Force); //Acceleration //Impulse //or change mass? //https://answers.unity.com/questions/789917/difference-and-uses-of-rigidbody-force-modes.html

        //TODO: better controlls... dont use addforce? more gravity?
    }
}
