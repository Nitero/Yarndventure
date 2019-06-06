using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : AbstractMesh
{
    //public Mesh mesh;

    public override float x(Vector2 uv)
    {
        return uv.x;
    }

    public override float y(Vector2 uv)
    {
        return uv.y;
    }

    public override float z(Vector2 uv)
    {
        return 0;
    }
}
