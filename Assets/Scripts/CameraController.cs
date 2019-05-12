using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform ballToFollow;
    [SerializeField] private float lookSpeed = 3; //slider instead?
    [SerializeField] private float horizontalCameraOffset = 3;
    [SerializeField] private float verticalCameraOffset = 3;

    [SerializeField] private float maxLensDistort = 20; //https://docs.unity3d.com/Packages/com.unity.postprocessing@2.0/manual/Manipulating-the-Stack.html
    [SerializeField] private float distortMulti = 2;
    private LensDistortion distortion;
    private PlayerController player;

    private void Start()
    {
        LockMouse();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        PostProcessVolume volume = gameObject.GetComponent<PostProcessVolume>();
        distortion = volume.profile.GetSetting<LensDistortion>();
    }

    private void Update()
    {
        // Rotate around ball horizontally (Deternibes what is forward)
        transform.RotateAround(ballToFollow.position, Vector3.up, Input.GetAxis("Mouse X"));

        // Look up and down
        transform.RotateAround(ballToFollow.position, -Camera.main.transform.right, Input.GetAxis("Mouse Y"));

        // Keep up with ball and move back so you can see it
        transform.position = ballToFollow.position - transform.forward * horizontalCameraOffset + transform.up * verticalCameraOffset; //Maybe get this offset as a vec3 in inspector

        //TODO: Stop rotating into floor, walls, etc

        //Get the speed of player and set lens distort accordingly
        var vel = player.GetVelocity() * distortMulti;
        if (vel >= maxLensDistort) vel = maxLensDistort;
        distortion.intensity.value = -vel; //TODO: interpolate
    }

    public void LockMouse() //TODO: Move this to somewhere else
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResetCamera()
    {
        transform.rotation = Quaternion.identity;
    }

    public void LevelCompleted()
    {
        GameObject.FindGameObjectWithTag("GameplayManager").GetComponent<ParticleManager>().SpawnGoalFX(transform.position);
        distortion.intensity.value = 0;
        this.enabled = false;
    }
}
