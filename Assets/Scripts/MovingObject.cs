using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    //Rather use dotween? can work with rb too !!!!!!!!!!!!!!!!!!!!!

    //private Rigidbody rb;
    public Vector3 direction;
    public float distance; //before turn around

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + direction * Time.deltaTime;
    }
}
