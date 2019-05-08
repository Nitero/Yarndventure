using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraController camera;
    [SerializeField] private float fallDeath = -10;
    [SerializeField] private float holdRespawnDelay = 0.25f;



    private Vector3 spawnPos;

    void Start()
    {
        spawnPos = player.transform.position;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(restartTimer());
        else
            StopCoroutine(restartTimer());
    }

    void FixedUpdate()
    {
        if (player.transform.position.y < fallDeath)
            respawnPlayer();
    }

    public void respawnPlayer()
    {
        player.transform.position = spawnPos;
        player.stopMovement();
        player.destroyRope();
        player.clearLine();
        camera.resetCamera();
    }

    public void levelCleared()
    {
        respawnPlayer(); // TODO: instead show menu or go straight to next menu
    }


    private IEnumerator restartTimer()
    {
        yield return new WaitForSeconds(holdRespawnDelay);
        respawnPlayer();
    }
}
