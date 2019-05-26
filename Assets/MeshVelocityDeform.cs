using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshVelocityDeform : MonoBehaviour
{
    private Material mat;
    private Rigidbody rb;
    private Vector3 velocity;
    [SerializeField] private MeshRenderer meshRend;

    void Start()
    {
        if(GetComponent<MeshRenderer>() != null) mat = GetComponent<MeshRenderer>().material;
        if(meshRend != null) mat = meshRend.material;
        rb = GetComponent<Rigidbody>();
    }


    void LateUpdate()
    {

        velocity = rb.velocity;

        //------------ DO ALL OF THIS NONSESENSE INSTEAD WITH A PARENT OBJECT
        //transform.rotation=Quaternion.SetLookRotation(velocity);
        //Vector3 rotatedVector = Quaternion.AngleAxis(30, Vector3.up) * originalVector;
        //velocity = transform.eulerAngles * velocity;
        //velocity = transform.rotation * velocity;
        //velocity = Quaternion.Inverse(transform.rotation) * velocity;

        //velocity = transform.InverseTransformDirection(velocity); //this works, but a problem still in shader??

        //velocity = transform.rotation * rb.velocity;
        //velocity += transform.eulerAngles;
        //velocity = transform.InverseTransformDirection(velocity);



        mat.SetVector("Vector3_FF646A34", velocity);

        print(velocity);


        /*
        Vector3 velocity = (transform.position - _lastPosition) / Time.deltaTime;
        _lastPosition = transform.position;
        
        //Calculate the desired squash amount based on the current Y axis velocity.
        float targetSquash = -Mathf.Abs(velocity.y) * VelocityStretch;

        //Adjust the squash velocity.
        _squashVelocity += (targetSquash - _squash) * Strength * Time.deltaTime;

        //Apply dampening to the squash velocity.
        _squashVelocity = ((_squashVelocity / Time.deltaTime) * (1f - Dampening)) * Time.deltaTime;

        //Apply the velocity to the squash value.
        _squash += _squashVelocity;

        _mpb.SetFloat(SquashID, _squash);
         */



        //TODO: add statement for when squished (touching ground)
    }
}
