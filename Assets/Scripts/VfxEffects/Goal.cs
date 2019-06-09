using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Goal : MonoBehaviour
{
    [SerializeField] private VisualEffect goalEffect;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        goalEffect.SetVector3("Goal", transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        goalEffect.SetVector3("Player", player.transform.position);
    }
}