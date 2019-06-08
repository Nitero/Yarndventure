using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShowBehindWalls : MonoBehaviour
{
    private Material[] mat;
    [SerializeField] private MeshRenderer[] wallsToFade;

    void Start()
    {
        mat = new Material[wallsToFade.Length];

        for(int i = 0; i < wallsToFade.Length; i++)
        {
            mat[i] = wallsToFade[i].GetComponent<MeshRenderer>().material;
        }

    }


    void Update()
    {
        var _newPosition = transform.position;
        _newPosition.x += Mathf.Sin(Time.time) * Time.deltaTime;
        transform.position = _newPosition;

        for (int i = 0; i < mat.Length; i++)
        {
            Vector2 pos = transform.position;
            Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);

            mat[i].SetVector("_Vector4_41DBEAF6", new Vector4(viewportPoint.x, viewportPoint.y, 0, 0));
        }
    }
}
