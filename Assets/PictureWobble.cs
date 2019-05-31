using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureWobble : MonoBehaviour
{

    [SerializeField] private AnimationCurve test;

    // maybe use: https://docs.unity3d.com/Manual/class-CustomRenderTexture.html !!!!!!!!!!!!
    [SerializeField] private CustomRenderTexture wobbleTex;
    [SerializeField] private int resolution = 256;
    [SerializeField] private float speed = 10;
    [SerializeField] private float speedFalloff = 0.1f;
    [SerializeField] private float heightFalloff = 0.1f;
    [SerializeField] private int initialThickness = 10;
    private Material mat;
    private Texture2D texture;
    private int middle;
       
    private droplet oneDrop;
    private droplet oneDrop2;

    private class droplet
    {
        private int position;
        public float radius;
        private float speed;
        private int thickness;
        private Texture2D texture;
        private float height = 1f;

        public droplet(int pos, float rad, int thick, float spd, Texture2D tex)
        {
            position = pos;
            radius = rad;
            thickness = thick;
            speed = spd;
            texture = tex;
        }


        public void update()
        {
            speed -= Time.deltaTime * 0.1f; //speedFalloff

            radius += Time.deltaTime * speed;

            height -= Time.deltaTime * 0.1f; //heightFalloff

            texture.Circle(position, position, (int) radius, new Color(height, height, height, height));
            texture.Circle(position, position, (int) radius-thickness, Color.black);

            //texture.Circle(middle, middle, 64, Color.white);
            //texture.Circle(middle, middle, 32, Color.black);
        }
    }


    void Start()
    {
        if (GetComponent<MeshRenderer>() != null) mat = GetComponent<MeshRenderer>().material;


        // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
        texture = new Texture2D(resolution, resolution, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Point;

        // Set all black at start (stationary)
        for (int y = 0; y < texture.height; y++)
            for (int x = 0; x < texture.width; x++)
                texture.SetPixel(x, y, Color.black);         //texture.SetPixel(0, 0, Color(1.0, 1.0, 1.0, 0.5));

        texture.Apply();

        middle = texture.height / 2;



        oneDrop = new droplet(middle, 20, initialThickness, 33, texture);
        oneDrop2 = new droplet(middle, 0, initialThickness, 33, texture);
    }


    void LateUpdate()
    {

        //Do more drops... they start out high and get lower over time, but get thicker over time?

        // Do some kind of color blur (weichzeichner?)

        // RIPPLE LOGIC PROBABLY REALLY LOOK UP ONLINE

        oneDrop.update();
        oneDrop2.update();

        if (oneDrop.radius * 1.25f >= texture.height)
            oneDrop = new droplet(middle, 20, initialThickness, 33, texture);
        if (oneDrop2.radius * 1.25f >= texture.height)
            oneDrop2 = new droplet(middle, 0, initialThickness, 33, texture);



        texture.Apply(); // Apply all SetPixel calls

        mat.SetTexture("_Wobbl", texture);
        //mat.SetTexture("_Wobbl", wobbleTex);
        //mat.SetTexture("_ImageTex", texture); //To debug drawing

        //print(texture.get);
    }
}





public static class Tex2DExtension
{
    public static Texture2D Circle(this Texture2D tex, int x, int y, int r, Color color)
    {
        float rSquared = r * r;

        for (int u = 0; u < tex.width; u++)
        {
            for (int v = 0; v < tex.height; v++)
            {
                if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared) tex.SetPixel(u, v, color);
            }
        }

        return tex;
    }
}