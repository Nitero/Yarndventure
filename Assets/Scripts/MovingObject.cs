using UnityEngine;

public class MovingObject : MonoBehaviour
{
    // TODO: Rather use dotween? Can work with rb too!!!
    private Rigidbody rb;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float distance; //before turn around
    private Vector3 startPos;

    private void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
        }
        startPos = transform.position;
    }


    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + direction * Time.deltaTime;
        if (Vector3.Distance(startPos, transform.position) >= distance)
        {
            direction = -direction;
        }
    }

    public void Reset()
    {
        transform.position = startPos;
        transform.rotation = Quaternion.identity;
        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
