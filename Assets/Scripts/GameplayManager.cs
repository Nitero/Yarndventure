using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraController camera;
    [SerializeField] private ScoreMenu score;
    [SerializeField] private Timer timer;
    [SerializeField] private float fallDeath = -10;
    [SerializeField] private float holdRespawnDelay = 0.25f;

    private Vector3 spawnPos;

    void Start () {
        spawnPos = player.transform.position;
    }

    private void Update () {
        if (Input.GetKeyDown (KeyCode.R))
            StartCoroutine (restartTimer ());
        else
            StopCoroutine (restartTimer ());
    }

    void FixedUpdate () {
        if (player.transform.position.y < fallDeath)
            respawnPlayer ();
    }

    public void respawnPlayer () {
        foreach (MovingObject mo in GameObject.FindObjectsOfType<MovingObject> ()) {
            mo.reset ();
        }
        player.transform.position = spawnPos;
        player.stopMovement ();
        player.destroyRope ();
        player.clearLine ();
        camera.resetCamera ();
    }

    public void levelCleared () {
        score.gameObject.SetActive (true);
        score.showScreen ();
        camera.unlockMouse ();
        player.stopMovement ();
    }

    private IEnumerator restartTimer () {
        yield return new WaitForSeconds (holdRespawnDelay);
        respawnPlayer ();
    }
}