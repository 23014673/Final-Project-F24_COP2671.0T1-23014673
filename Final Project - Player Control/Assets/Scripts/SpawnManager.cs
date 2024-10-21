using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject applePrefab;

    private Vector3 spawnPos = new Vector3(25, 0, 0);

    private float startDelay = 2;
    private float repeatRate = 2;

    private PlayerController playerControllerScript;

    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);

            Vector3 applePos = new Vector3(obstacle.transform.position.x, obstacle.transform.position.y + 4, obstacle.transform.position.z);
            
            GameObject apple = Instantiate(applePrefab, applePos, applePrefab.transform.rotation);

            apple.transform.parent = obstacle.transform;
        }
    }
}
