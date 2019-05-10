using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMeshController : MonoBehaviour
{
    private Transform player;
    [SerializeField] private GameObject ropePrefab;
    [SerializeField] private GameObject ropeEnd;
    [SerializeField] private float maxLength = 100;
    [SerializeField] private float connectionSpeed = 1; //seconds it takes to get to actual hooked position

    private float currentTotalLength;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //Initialize rope
        for (int i = transform.childCount + 1; i < maxLength; i++)
        {
            var r = Instantiate(ropePrefab, transform.position + Vector3.up * -i, Quaternion.identity);
            r.transform.parent = transform;
        }
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        //Animate flying from player to anchor position
        //from: transform.localPosition = -transform.parent.position + player.position;
        //to:   transform.localPosition = Vector3.zero;
        float step = currentTotalLength * connectionSpeed * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, step);

        if (Vector3.Distance(transform.localPosition, Vector3.zero) <= 0.01)
            ropeEnd.SetActive(true);
        else
            ropeEnd.SetActive(false);


        //Look at rotation to player
        transform.LookAt(player.position);
        transform.eulerAngles += new Vector3(-90, 0, 0);

        //Enable as many children as needed via distance (shader would be better)
        var dist = Vector3.Distance(transform.position, player.transform.position);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (Mathf.RoundToInt(dist) > i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }


    public void setPlayerStartPos()
    {
        if (player == null) return;

        transform.localPosition = -transform.parent.position + player.position;
        currentTotalLength = Vector3.Distance(transform.localPosition, Vector3.zero);
    }
}
