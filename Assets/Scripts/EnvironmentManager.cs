using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject environment;
    public List<GameObject> activeEnvironments = new List<GameObject>();
    public float zSpawn;
    public float length;

    private int numberOfEnvironments;

    public Transform playerTransform;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        numberOfEnvironments = 2;
        zSpawn = 185;
        length = 190;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.HasGameStarted && Mathf.Abs(playerTransform.position.z) > (zSpawn - (numberOfEnvironments * length)))
        {
            SpawnEnvironment();
        }
    }

    private void SpawnEnvironment()
    {
        Instantiate(environment, -(environment.transform.right * zSpawn), environment.transform.rotation);
        zSpawn += length;
    }

    public void BatchSpawnEnvironment()
    {
        for (int i = 0; i < numberOfEnvironments; i++)
        {
            SpawnEnvironment();
        }
    }
}
