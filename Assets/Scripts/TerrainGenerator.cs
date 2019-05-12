using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject defaultCube;
    public Vector3 centerOfMap = new Vector3(0, 0, 25);
    public float safeRadius = 100;

    public int ammountOfSmallCubes = 50;
    public int ammountOfCubes = 10;
    public float maxDist = 200;
    public float fogPosition = -15;
    public int seed;
    public bool seedByLevel = false;

    private void Start()
    {
        var par = new GameObject("Environment");

        par.layer = 2; //Ignore raycasts

        if (seed != 0)
        {
            Random.seed = seed;
        }
        if (seedByLevel)
        {
            Random.seed = SceneManager.GetActiveScene().buildIndex + seed;
        }

        for (int i = 0; i < ammountOfCubes; i++)
        {
            var randPos = centerOfMap + new Vector3(Random.Range(-maxDist, maxDist), 0, Random.Range(-maxDist, maxDist));

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
