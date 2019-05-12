using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manages updates to the state and view of a level
public class GameplayManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraController camera;
    [SerializeField] private float fallDeath = -10;
    [SerializeField] private float holdRespawnDelay = 0.25f;

    private LevelUI levelUI;
    private Score score;
    private Timer timer;
    private ScreenShakeTest screnshake;
    private Vector3 spawnPos;

    private void Start()
    {
        timer = GameObject.FindObjectOfType<Timer>();
        spawnPos = player.transform.position;
        screnshake = Camera.main.GetComponent<ScreenShakeTest>();

        score = new Score();
        levelUI = GameObject.FindGameObjectWithTag("LevelUI").GetComponent<LevelUI>();
        levelUI.SelectGamePlayUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(RestartTimer());
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            StopAllCoroutines();//StopCoroutine(RestartTimer());
        }
    }

    private void FixedUpdate()
    {
        if (player.transform.position.y < fallDeath && player.GetMovementState())
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {

        foreach (MovingObject mo in GameObject.FindObjectsOfType<MovingObject>())
        {
            mo.Reset();
        }
        player.transform.position = spawnPos;
        player.StopMovement();
        player.DestroyRope();
        player.ClearLine();
        camera.ResetCamera();
        timer.ResetTime();

        screnshake.AddShake(Vector2.up, 0.5f);
        GameObject.FindGameObjectWithTag("GameplayManager").GetComponent<ParticleManager>().PlayerRespawn();
    }

    public void LevelCleared()
    {
        levelUI.SelectScoreMenuUI();
        if (score.UpdateScore(timer.GetCurrentTime())) {
            levelUI.SetScoreUIWithNewRecord(score.GetBestTime());
        } else {
            levelUI.SetScoreUI(timer.GetCurrentTime(), score.GetBestTime());
        }
        camera.UnlockMouse();
    }

    private IEnumerator RestartTimer()
    {
        yield return new WaitForSeconds(holdRespawnDelay);
        RespawnPlayer();
    }
}