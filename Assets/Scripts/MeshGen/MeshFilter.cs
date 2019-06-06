using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MeshTypeFilter
{
    //Mesh mesh;

    float x(Vector2 uv);
    float y(Vector2 uv);
    float z(Vector2 uv);


}
