using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshVelocityDeform : MonoBehaviour
{
    private Material mat;
    private Rigidbody rb;
    private Vector3 velocity = new Vector3(2,1,1);

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        velocity = rb.velocity;
        mat.SetVector("Vector3_FF646A34", velocity);

        print(velocity);
    }
}
