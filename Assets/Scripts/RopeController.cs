using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField]
    private float shootRange = 100;
    [SerializeField]
    private GameObject anchorGO;
    private Rigidbody anchorRB;
    private ConfigurableJoint joint;

    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        anchorRB = anchorGO.GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && joint == null)
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity/*shootRange*/) && hit.collider)
            {
                anchorGO.transform.position = hit.point;

                anchorGO.SetActive(true);
                gameObject.AddComponent<ConfigurableJoint>();
                joint = GetComponent<ConfigurableJoint>();
                joint.autoConfigureConnectedAnchor = false;
                joint.axis = Vector3.zero;
                joint.anchor = Vector3.zero;
                joint.secondaryAxis = Vector3.zero;
                joint.connectedAnchor = Vector3.zero;
                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Limited;
                joint.zMotion = ConfigurableJointMotion.Limited;
                SoftJointLimit softJointLimit = new SoftJointLimit();
                softJointLimit.limit = Vector3.Distance(anchorGO.transform.position, transform.position);
                joint.linearLimit = softJointLimit;
                joint.connectedBody = anchorRB;
            }
        }

        if (Input.GetMouseButton(0) && joint != null) //While held down the rope can get shorter, but not longer 
        {
            SoftJointLimit softJointLimit = new SoftJointLimit();
            softJointLimit.limit = Vector3.Distance(anchorGO.transform.position, transform.position);
            joint.linearLimit = softJointLimit;

            //TODO: Maybe shrink over time or with mouse wheel manually?
        }

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(joint);
            anchorGO.SetActive(false);
        }
    }
}
