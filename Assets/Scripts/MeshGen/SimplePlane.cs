using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlane : MonoBehaviour
{
    //public Mesh mesh;

    public float x(Vector2 uv)
    {
        return uv.x;
    }

    public float y(Vector2 uv)
    {
        return uv.y;
    }

    public float z(Vector2 uv)
    {
        return 0;
    }
}
