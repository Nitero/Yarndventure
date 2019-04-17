using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAttacher : MonoBehaviour
{

    public Transform attachedObj;
    //TODO: add optional offset

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }


    void LateUpdate()
    {
        line.SetPosition(1, attachedObj.position);
    }
}
