using UnityEngine;
using UnityEngine.Experimental.VFX;

public class PlayerController : MonoBehaviour
{
    private RopeController rope;
    [SerializeField] private VisualEffect fireEffect;
    [SerializeField] private float fireTresh = 5f;
    [SerializeField] private float airVelocityMax = 10f;
    [SerializeField] private float slowSpeed = 10f;
    [SerializeField] private float fastSpeed = 20f;

    [SerializeField] private float drag = 3f;
    [SerializeField] private float dragWithRope = 0f;

    private Vector3 pos;
    private float moveSpeed;
    private float currentSpeed;
    private Rigidbody rb;
    private CameraController cam;
    private bool activateMovement;

    const float EPSILON = 0.005f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rope = GetComponent<RopeController>();
        cam = Camera.main.GetComponent<CameraController>();
        pos = transform.position;
        activateMovement = true;
        //fireEffect.SendEvent("OnStop");
        //fireEffect.Stop();
        //fireEffect.SetBool("OnStop", true);
        fireEffect.SetFloat("Partics", 0);
    }

    private void Update() //TODO: don't change rigidbody here!
    {
        if (activateMovement)
        {
            currentSpeed = (transform.position - pos).magnitude;
            pos = transform.position;

            if (Input.GetKey(KeyCode.Space))
            {// && Input.GetKey (KeyCode.W)) {

                //fireEffect.SendEvent("OnPlay");
                //fireEffect.Play();

                moveSpeed = fastSpeed;
                // }  else if (Input.anyKey == false && currentSpeed >= EPSILON) {     
                // maybe a formular for slowing down slowly, when no key is pressed?
            }
            else if (Input.anyKey == false && currentSpeed < EPSILON)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            else
            {
                moveSpeed = slowSpeed;
            }

            if (Input.GetMouseButton(0))
            {
                rb.drag = dragWithRope;
            }
            else
            {
                rb.drag = drag; //maybe after using rope, slowly go down to normal drag
            }
        }

        if (rb.velocity.magnitude >= fireTresh) fireEffect.SetFloat("Partics", 10 * rb.velocity.magnitude * rb.velocity.magnitude);
        else fireEffect.SetFloat("Partics", 0);
    }

    private void FixedUpdate()
    {
        float vertInput = Input.GetAxis("Vertical");
        float horInput = Input.GetAxis("Horizontal");

        //Get current orientation of camera to know where pressign W should make ball go
        //Only x & z values only are wanted, so ignore camera looking into ground

        Vector3 flattendForwardLookDir = Camera.main.transform.forward;
        flattendForwardLookDir.y = 0;
        flattendForwardLookDir.Normalize();

        Vector3 rightMoveDir = Vector3.Cross(Vector3.up, flattendForwardLookDir);

        Vector3 moveDir = (flattendForwardLookDir * vertInput) + (rightMoveDir * horInput);
        moveDir.Normalize();

        rb.AddForce(moveDir * moveSpeed, ForceMode.Force); //https://answers.unity.com/questions/789917/difference-and-uses-of-rigidbody-force-modes.html


        //Additional boost on using space (once when pressed)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Mix out of camera look dir and moving direction... maybe just one of either?
            //rb.AddForce(moveDir * (fastSpeed / 8), ForceMode.Impulse);
            //rb.AddForce(Camera.main.transform.forward * (fastSpeed / 8), ForceMode.Impulse);

            rb.AddForce(moveDir * (fastSpeed / 4), ForceMode.Impulse); //More intuitive?
            //rb.AddForce(Camera.main.transform.forward * (fastSpeed / 4), ForceMode.Impulse);
        }


        //Limit horizontal velocity (also do same for air? but with a little faster?)
        /*var horMove = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (horMove.magnitude > moveSpeed)
            rb.velocity = horMove.normalized * moveSpeed;
        rb.velocity = horMove + new Vector3(0, rb.velocity.y, 0);*/

        //TODO: better controlls... dont use addforce? more mass! 
    }

    public void StopMovement()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public void DestroyRope()
    {
        rope.Destroy();
    }

    public void ClearLine()
    {
        GetComponentInChildren<TrailRenderer>().Clear();
    }

    public bool GetMovementState()
    {
        return activateMovement;
    }

    public float GetVelocity()
    {
        return rb.velocity.magnitude;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            GameObject.FindGameObjectWithTag("GameplayManager").GetComponent<GameplayManager>().LevelCleared();
            activateMovement = false;
            //StopMovement ();
            Camera.main.GetComponent<CameraController>().LevelCompleted();
            Camera.main.GetComponent<ScreenShakeTest>().LevelCompleted();
            moveSpeed = 0;
        }
    }
}