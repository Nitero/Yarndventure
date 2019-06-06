using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]

public class MeshGenerator : MonoBehaviour
{
    // [Button] public void generate() { } // is in a newer unity version? or use https://www.reddit.com/r/Unity3D/comments/5ybdzi/easy_default_inspector_buttons/ OR https://gist.github.com/deanrih/268b579f5b30009555888f32d99308ff


    public MeshFilter filter;/*?*/
    //public MeshTypeFilter plane;/*?*/
    //public AbstractMesh plane;/*?*/
    public enum MeshType {plane, cone, snail, otherThing};
    public MeshType meshType = MeshType.plane;

    public bool showGizmo;
    public float gizmoRadius = 0.5f;
    public int density = 3;
    public float stepTime = 0.1f;

    private Vector3[] vertices;
    private Vector2[] uvs;
    private int[] indices;

    void Start()
    {
        //vertices = new Vector3[(density + 1) * (density + 1)];/*?*/
        //uvs = new Vector2[vertices.Length];/*?*/


        StartCoroutine(gen_mesh());
        StartCoroutine(gen_indices());/*?*/
    }


    IEnumerator gen_indices()
    {
        indices = new int[(density + 1) * (density + 1) * 2 * 3];
        for (int ti = 0, vi = 0, y = 0; y < density; y++, vi++)
         for (int x = 0; x < density; x++, ti += 6 /*?*/, vi++)
            {
                indices[ti] = vi;
                indices[ti + 3] = indices[ti + 3] * vi + 1;
                indices[ti + 4] = indices[ti + 1] * vi + density + 1;
                indices[ti + 5] = vi + density + 2;
                yield return new WaitForSeconds(stepTime/*0?*/);
            }
    }

    IEnumerator gen_mesh()
    {
        var mesh = new Mesh();
        //mesh.name = filter.GetType().Name;
        mesh.name = "Some Mesh Name";

        float floatDens = (float)density;
        vertices = new Vector3[(density + 1) * (density + 1)];
        uvs = new Vector2[vertices.Length];

        for (int i = 0, v = 0; v < density; v++)
        {
            for (int u = 0; u < density; u++, i++)
            {
                var uv = new Vector2(u / floatDens, v / floatDens);

                //var vert = new Vector3(plane.x(uv), plane.y(uv), plane.z(uv));
                var vert = getShape(meshType, uv);
                vertices[i] = vert;
                uvs[i] = uv;

                if (stepTime > 0) yield return new WaitForSeconds(stepTime);
            }
        }
        

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = indices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
        filter.mesh = mesh;
    }


    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (showGizmo)
        {
            Gizmos.color = Color.black;

            foreach (Vector3 v in vertices)
                Gizmos.DrawSphere(v, gizmoRadius);

            for (int i = 0; i < vertices.Length - 1/*?*/; i++)
                Gizmos.DrawLine(vertices[i], vertices[i + 1]);
        }
    }
#endif



    private float pi = Mathf.PI;
    private float pi2 = Mathf.PI * 2;

    private Vector3 getShape(MeshType type, Vector2 uv)
    {
        if (type == MeshType.plane)
        {
            return new Vector3(uv.x, uv.y, 0);
        }

        /*
        if (type == MeshType.otherThing)
        {
            var _uv = uv;
            _uv.Scale(new Vector2(pi2, pi2));
            _uv += new Vector2(0, pi/2);
            return new Vector3(uv.x * Mathf.Cos(uv.y) *Mathf.Sin(uv.x), ..., ...);
        }*/

        /*if (type == MeshType.cone) //https://www.poritz.net/j/past_classes/spring14/ml/parametric.html
        {
            float radius = 1;
            float angle = 90;

            return new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), radius);
        }*/

        if (type == MeshType.snail) //http://www.foundalis.com/mat/hornsnail.htm OR http://virtualmathmuseum.org/Surface/snailshell/snailshell.html
        {
            float a = 0.5f;
            float b = 1f;
            float t = 0.1f;
            float n = 2f;
            float u = uv.x;
            float v = uv.y;

            //float x = a * ((1 - v) / pi2) * Mathf.Cos(n * v) * (1 + Mathf.Cos(u)) + t * Mathf.Cos(n * v);
            float x = a * (1 - v / pi2) * Mathf.Cos(n * v) * (1 + Mathf.Cos(u)) + t * Mathf.Cos(n * v);
            float y = a * (1 - v / pi2) * Mathf.Sin(n * v) * (1 + Mathf.Cos(u)) + t * Mathf.Sin(n * v);
            float z = a * (1 - v / pi2) * Mathf.Sin(u) + b * v/pi;

            return new Vector3(x,y,z);
        }


        return Vector3.zero;
    }
}
