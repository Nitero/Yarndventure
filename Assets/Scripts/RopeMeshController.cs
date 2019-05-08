using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMeshController : MonoBehaviour
{
    private Transform player;

    [SerializeField] private GameObject ropePrefab;
    [SerializeField] private float maxLength = 100;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //Initialize rope
        for (int i = transform.childCount+1; i < maxLength; i++)
        {
            var r = Instantiate(ropePrefab, transform.position + Vector3.up * - i, Quaternion.identity);
            r.transform.parent = transform;
        }
    }


    void Update()
    {
        
    }


    void LateUpdate()
    {
        //Look at rotation to player
        transform.LookAt(player.position);
        transform.eulerAngles += new Vector3(-90, 0, 0);

        //Enable as many children as needed via distance (shader would be better)
        var dist = Vector3.Distance(transform.position, player.transform.position);

        for (int i = 0; i < transform.childCount; i++)
        {
            if(Mathf.RoundToInt(dist) > i)
                transform.GetChild(i).gameObject.SetActive(true);
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }

    }
}
