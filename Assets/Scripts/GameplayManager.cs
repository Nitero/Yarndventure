using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraController camera;
    private ScoreMenu scoreMenu;
    [SerializeField] private float fallDeath = -10;
    [SerializeField] private float holdRespawnDelay = 0.25f;

    private Vector3 spawnPos;

    void Start()
    {
        spawnPos = player.transform.position;
        scoreMenu = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreMenu>();
        scoreMenu.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(RestartTimer());
        }
        else
        {
            StopCoroutine(RestartTimer());
        }
    }

    void FixedUpdate()
    {
        if (player.transform.position.y < fallDeath)
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
    }

    public void LevelCleared()
    {
        scoreMenu.gameObject.SetActive(true);
        scoreMenu.ShowScreen();
        camera.UnlockMouse();
    }

    private IEnumerator RestartTimer()
    {
        yield return new WaitForSeconds(holdRespawnDelay);
        RespawnPlayer();
    }
}