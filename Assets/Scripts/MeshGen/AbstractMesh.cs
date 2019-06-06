using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMesh : MonoBehaviour, MeshTypeFilter
{


    public abstract float x(Vector2 uv);
    public abstract float y(Vector2 uv);
    public abstract float z(Vector2 uv);


    /*
    public float x(Vector2 uv)
    {
        return uv.x; // default = plane
    }

    public float y(Vector2 uv)
    {
        return uv.y; // default = plane
    }

    public float z(Vector2 uv)
    {
        return 0; // default = plane
    }*/
}
