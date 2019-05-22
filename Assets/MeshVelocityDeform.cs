using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshVelocityDeform : MonoBehaviour
{
    private Material mat;
    private Rigidbody rb;
    private Vector3 velocity;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        velocity = rb.velocity;

        //transform.rotation=Quaternion.SetLookRotation(velocity);
        //Vector3 rotatedVector = Quaternion.AngleAxis(30, Vector3.up) * originalVector;
        //velocity = transform.eulerAngles * velocity;
        //velocity = transform.rotation * velocity;
        //velocity = Quaternion.Inverse(transform.rotation) * velocity;

        //velocity = transform.InverseTransformDirection(velocity); //this works, but a problem still in shader??



        // lets say ball rolls right vel(1,0), but ball is rotated 90 on z axis -> vel should now be (0,-1)
        // and inbetwee at 45 degress vel(0.5,-0.5)
        // HOW?????? ask can


        //velocity = transform.rotation * rb.velocity;
        //velocity += transform.eulerAngles;
        //velocity = transform.InverseTransformDirection(velocity);


        mat.SetVector("Vector3_FF646A34", velocity);

        print(velocity);
    }
}
