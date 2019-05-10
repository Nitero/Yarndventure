using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject defaultCube;

    //public Vector3[] safeZone = new Vector3[] {new Vector3(50,0,-50), new Vector3(-50, 0, -50), new Vector3(50, 0, 100), new Vector3(-50, 0, 100) };
    public Vector3 centerOfMap = new Vector3(0, 0, 25);
    public float safeRadius = 100;

    public int ammountOfSmallCubes = 50;
    public int ammountOfCubes = 10;
    public float maxDist = 200;
    public float fogPosition = -15;
    public int seed;
    public bool rand = true;
    public bool seedByLevel = false;

    void Start()
    {
        //center = new Vector3((safeZone[0].x + safeZone[1].x + safeZone[2].x + safeZone[3].x) / 4, 0, (safeZone[0].z + safeZone[1].z + safeZone[2].z + safeZone[3].z) / 4);
        //w = safeZone[0].x + safeZone[1].x;
        //h = safeZone[0].z + safeZone[3].z;

        var par = new GameObject("Environment");

        par.layer = 2; //Ignore raycasts

        if (!rand)
        {
            Random.seed = seed;//(int)System.DateTime.Now.Ticks;
        }

        for (int i = 0; i < ammountOfCubes; i++)
        {
            var randPos = centerOfMap + new Vector3(Random.Range(-maxDist, maxDist), 0, Random.Range(-maxDist, maxDist));

            //Rect rect = new Rect(center.x, center.z, w, h); //safeZone
            while (Vector3.Distance(randPos, centerOfMap) < safeRadius)
            {
                randPos = centerOfMap + new Vector3(Random.Range(-maxDist, maxDist), 0, Random.Range(-maxDist, maxDist));
            }

            var c = Instantiate(defaultCube, randPos, Quaternion.identity);
            c.transform.parent = par.transform;
            c.transform.localScale = new Vector3(Random.Range(25, 100), Random.Range(50, 200), Random.Range(25, 100));
            c.transform.position -= new Vector3(0, Random.Range(25, 75), 0);
            c.transform.eulerAngles = new Vector3(Random.Range(-10, 10), Random.Range(0, 360), Random.Range(-10, 10));
        }

        for (int i = 0; i < ammountOfSmallCubes; i++)
        {
            var randPos = centerOfMap + new Vector3(Random.Range(-maxDist, maxDist), 0, Random.Range(-maxDist, maxDist));

            while (Vector3.Distance(randPos, centerOfMap) < safeRadius)
            {
                randPos = centerOfMap + new Vector3(Random.Range(-maxDist, maxDist), 0, Random.Range(-maxDist, maxDist));
            }

            var c = Instantiate(defaultCube, randPos, Quaternion.identity);
            c.transform.parent = par.transform;
            c.transform.localScale = new Vector3(Random.Range(25, 50), Random.Range(50, 150), Random.Range(25, 50));
            c.transform.position -= new Vector3(0, Random.Range(25, 50), 0);
            c.transform.eulerAngles = new Vector3(Random.Range(-30, 30), Random.Range(0, 360), Random.Range(-30, 30));
        }

    }
}
