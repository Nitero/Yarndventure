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
        //get current orientation of camera to know where pressign W should make ball go

        Vector3 flattendForwardLookDir = Camera.main.transform.forward; //x und z werte, zeigen aber leicht in boden, wie kamera... daher normalisieren um nur auf boden entlang zu bewegen
        flattendForwardLookDir.y = 0;
        flattendForwardLookDir.Normalize();

        //print(flattendForwardLookDir);

        float speed = Input.GetAxis("Vertical"); //forward, backwards
        float speedSideways = Input.GetAxis("Horizontal"); //left, right

        rb.AddForce(flattendForwardLookDir * speed * moveSpeed, ForceMode.Impulse);


        var right = Vector3.Cross(Vector3.up, flattendForwardLookDir);

        rb.AddForce(right * speedSideways * moveSpeed, ForceMode.Impulse);

        //TODO: dont use addforce
    }
}
