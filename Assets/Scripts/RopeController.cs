using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField]
    private GameObject anchorGO;
    private Rigidbody anchorRB;
    private SpringJoint joint;

    void Start()
    {
        joint = GetComponent<SpringJoint>();
        anchorRB = anchorGO.GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.GetMouseButton(0)) //TODO: spawn anchor at mouse position (if raycast hits) and configure joint
        {
            if (joint == null)
            {
                anchorGO.SetActive(true);
                gameObject.AddComponent<SpringJoint>();
                joint = GetComponent<SpringJoint>();
                joint.anchor = Vector3.zero;
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = Vector3.zero;
                joint.spring = 10; //somehow calcualte from distance? Vector3.Distance(anchorGO.transform.position, transform.position)
                joint.connectedBody = anchorRB;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(joint);
            anchorGO.SetActive(false);
        }
    }
}
