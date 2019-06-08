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
    public Material[] mats;

    public bool autoGenOnChange = true;
    public bool showGizmo;
    public float gizmoRadius = 0.5f;
    public int density = 3;
    //public int densityX = 3;
    //public int densityY = 3;
    //public int densityZ = 3;
    public float stepTime = 0.1f;



    public enum MeshType { plane, perlinLandscape, cone, ball, snail, snail2 /*, dini*/ };
    [Space]
    [Header("Change these two:")]
    public MeshType meshType = MeshType.plane;

    public enum MatType { lit, waterShader, fog };
    public MatType materialType = MatType.lit;
    private MatType currMatType;



    private Mesh mesh;
    private MeshRenderer meshRend;
    private Vector3[] vertices;
    private Vector2[] uvs;
    private int[] triangIndices;

    [Space]
    [Header("Type parameters:")]
    public float planeUV = 1f;
    [Space]
    public float perlinDens = 1f;
    public float perlinHeight = 1f;
    public float perlinUV = 1f;
    [Space]
    public float coneRadius = 1;
    public float coneUV = 1;
    [Space]
    public float ballUV = 6.283185f; // 2 pi
    public float ballScale = 1;
    [Space]
    public float diniA = 1;
    public float diniB = 0.3f;
    public float diniUV = 1;
    [Space]
    public float[] snailParam = new float[] { 0.5f, 1f, 0.1f, 2f, 1f};
    [Space]
    public float[] snail2Param = new float[] { 1f, 0.5f, 1f, 1f, -2f, 1f};



    void Start()
    {
        mesh = new Mesh();
        mesh.name = meshType.ToString();
        filter.mesh = mesh;

        meshRend = GetComponent<MeshRenderer>();
        meshRend.material = mats[(int)materialType];
        currMatType = materialType;

        StartCoroutine(gen_mesh());
    }


    private void Update()
    {
        if(autoGenOnChange)//TODO: if the values changed
        {
            generateMesh();
        }

        if(currMatType != materialType)
        {
            currMatType = materialType;
            meshRend.material = mats[(int)materialType];
        }

        updateMesh();
    }


    IEnumerator gen_mesh()
    {
        float floatDens = (float)density;
        vertices = new Vector3[(density + 1) * (density + 1)];
        uvs = new Vector2[vertices.Length];

        //for (int i = 0, v = 0; v < density; v++)
        //    for (int u = 0; u < density; u++, i++)
        for (int i = 0, v = 0; v <= density; v++)
            for (int u = 0; u <= density; u++, i++)
            {
                var uv = new Vector2(u / floatDens, v / floatDens);

                vertices[i] = getShape(meshType, uv); // Get a vert at this uv position
                uvs[i] = uv;

                //if (stepTime > 0) yield return new WaitForSeconds(stepTime);
            }


        /*
        triangIndices = new int[(density + 1) * (density + 1) * 2 * 3];
        for (int ti = 0, vi = 0, y = 0; y < density; y++, vi++)
            for (int x = 0; x < density; x++, ti += 6, vi++)
            {
                triangIndices[ti] = vi;
                triangIndices[ti + 3] = triangIndices[ti + 3] * vi + 1;
                triangIndices[ti + 4] = triangIndices[ti + 1] * vi + density + 1;
                triangIndices[ti + 5] = vi + density + 2;

                if (stepTime > 0) yield return new WaitForSeconds(stepTime);
            }
        */


        // https://www.youtube.com/watch?v=64NblGkAabk    
        
        triangIndices = new int[density * density * 6];
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < density; z++)
        {
            for (int x = 0; x < density; x++)
            {
                triangIndices[tris + 0] = vert + 0;
                triangIndices[tris + 1] = vert + density + 1;
                triangIndices[tris + 2] = vert + 1;
                triangIndices[tris + 3] = vert + 1;
                triangIndices[tris + 4] = vert + density + 1; 
                triangIndices[tris + 5] = vert + density + 2; 

                vert++;
                tris += 6;

                if (stepTime > 0) yield return new WaitForSeconds(stepTime);
            }
            vert++;
        }
    }



    public void generateMesh()
    {
        StartCoroutine(gen_mesh());
    }


    private void updateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangIndices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
        //Do all of these have to be updated every frame? Just after generation?
    }



    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (showGizmo && vertices != null)
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
            return new Vector3(uv.x * planeUV, 0, uv.y * planeUV);
        }

        if (type == MeshType.perlinLandscape)
        {
            float u = uv.x * perlinUV;
            float v = uv.y * perlinUV;

            return new Vector3(u, Mathf.PerlinNoise(u * perlinDens, v * perlinDens) * perlinHeight, v);
        }

        if (type == MeshType.cone) //https://www.poritz.net/j/past_classes/spring14/ml/parametric.html
        {
            //coneScale

            float phi = uv.x * coneUV;
            float theta = uv.y * coneUV;

            float x = coneRadius * phi * Mathf.Cos(theta);
            float y = coneRadius * phi;
            float z = coneRadius * phi * Mathf.Sin(theta);

            return new Vector3(x,y,z);
        }

        if (type == MeshType.ball)
        {
            float phi = uv.x * ballUV;
            float theta = uv.y * ballUV;

            float x = ballScale * Mathf.Sin(phi)* Mathf.Cos(theta);
            float y = ballScale * Mathf.Cos(phi);
            float z = ballScale * Mathf.Sin(phi)* Mathf.Sin(theta);

            return new Vector3(x,y,z);
        }


        if (type == MeshType.snail) //http://www.foundalis.com/mat/hornsnail.htm
        {
            float a = snailParam[0];
            float b = snailParam[1];
            float t = snailParam[2];
            float n = snailParam[3];
            float u = uv.x * snailParam[4];
            float v = uv.y * snailParam[4];

            //float x = a * ((1 - v) / pi2) * Mathf.Cos(n * v) * (1 + Mathf.Cos(u)) + t * Mathf.Cos(n * v);
            float x = a * (1 - v / pi2) * Mathf.Cos(n * v) * (1 + Mathf.Cos(u)) + t * Mathf.Cos(n * v);
            float y = a * (1 - v / pi2) * Mathf.Sin(n * v) * (1 + Mathf.Cos(u)) + t * Mathf.Sin(n * v);
            float z = a * (1 - v / pi2) * Mathf.Sin(u) + b * v/pi;

            return new Vector3(x,y,z);
        }

        if (type == MeshType.snail2) //http://virtualmathmuseum.org/Surface/snailshell/snailshell.html
        {
            float aa = snail2Param[0];
            float bb = snail2Param[1];
            float cc = snail2Param[2];
            float dd = snail2Param[3];
            float ee = snail2Param[4];
            float u = uv.x * snail2Param[5];
            float v = uv.y * snail2Param[5];
            float vv = v + Mathf.Pow((v + ee), 2/16);
            float s = Mathf.Exp(-cc * vv);
            float r = s * (aa + bb * Mathf.Cos(u));

            float x = r * Mathf.Cos(vv);
            float y = r * Mathf.Sin(vv);
            float z = dd *(1-s) + s * bb * Mathf.Sin(u);


            return new Vector3(x, y, z);
        }

        // crashes...
        /*if (type == MeshType.dini) //https://en.wikipedia.org/wiki/Dini%27s_surface
        {
            float a = diniA;
            float b = diniB;
            float u = uv.x * diniUV;
            float v = uv.y * diniUV;

            float x = a * Mathf.Cos(u) * Mathf.Sin(v);
            float y = a * (Mathf.Cos(v) + Mathf.Log(Mathf.Tan(v / 2))) + b * u; 
            float z = a * Mathf.Sin(u) * Mathf.Sin(v);

            return new Vector3(x, y, z);
        }*/


        //More:
        //https://en.wikipedia.org/wiki/Parametric_surface
        //https://en.wikipedia.org/wiki/Klein_bottle
        //https://en.wikipedia.org/wiki/Boy%27s_surface
        //https://www.grasshopper3d.com/profiles/profile/show?id=parametric&


        return Vector3.zero;
    }


}
