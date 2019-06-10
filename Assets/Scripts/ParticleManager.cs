using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private float playerFastThreshold = 5f; //after which velocity show effect
    [SerializeField] private GameObject konfetti;
    [SerializeField] private GameObject playerBoostScreen;
    [SerializeField] private GameObject playerRespawnPart;

    private PlayerController player;
    private ParticleSystem windTrail;

    private void Start()
    {
        if(GameplayManager.isTechDemo()) {
            return;
        } 

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        windTrail = Camera.main.GetComponentInChildren<ParticleSystem>();
        
    }

    private void Update()
    {

        if(GameplayManager.isTechDemo())
        {
            return;
        }

        if (player.GetVelocity() >= playerFastThreshold)
        {
            //windTrail.Play();
        }

        if (Input.GetKeyDown(KeyCode.Space)) //Wind trails based on how fast player is
        {
            int ammount = Mathf.RoundToInt(player.GetVelocity() * 1.5f);
            if (ammount >= 30) ammount = 30;
            for (int i = 0; i < ammount; i++)
            {
                var b = Instantiate(playerBoostScreen, Camera.main.transform.position, Quaternion.identity);
                b.transform.parent = Camera.main.transform;
                b.transform.rotation = Camera.main.transform.rotation;
            }
        }
    }

    public void SpawnGoalFX(Vector3 cameraPos)
    {
        var k = Instantiate(konfetti, cameraPos, Quaternion.identity);
        k.transform.position += new Vector3(0, -1, 1);
        //k.transform.parent = Camera.main.transform;
    }

    public void PlayerRespawn()
    {
        Instantiate(playerRespawnPart, player.transform.position, Quaternion.identity);
    }
}
