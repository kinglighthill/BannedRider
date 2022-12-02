using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] vehiclePrefabs;
    public GameObject coinPrefab;

    public Transform playerTransform;
    private float offsetZVehicle;
    private float offsetZCoin;

    private float vehicleSpawnRangeX = 5f;
    private float vehicleSpawnPosZ = 50.0f;
    private int vehicleMaxSpawn = 100;


    private float coinSpanwRangeX = 5f;
    private float coinSpanwRangeZ = 1f;
    private float coinSpawnRate = 2f;
    private int coinMaxSpawnPerTime = 5;
    private int coinMaxSpawn = 20;

    private int count = 0;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        offsetZVehicle = offsetZCoin = 0.0f;


        //for (int i = 0; i < vehicleMaxSpawn; i++)
        //{
        //    SpawnVehicle();
        //}

        for (int i = 0; i < coinMaxSpawn; i++)
        {
            SpawnCoin();
        }
    }

    // Update is called once per frame
    void Update()
    {

        //InvokeRepeating("SpawnCoin", 0, 2);
    }

    void SpawnVehicle()
    {
        offsetZVehicle += vehicleSpawnPosZ;
        Vector3 spawnPos = new Vector3(Random.Range(-vehicleSpawnRangeX, vehicleSpawnRangeX), 0, offsetZVehicle);
        int vehicleIndex = Random.Range(0, vehiclePrefabs.Length);

        Instantiate(vehiclePrefabs[vehicleIndex], spawnPos, vehiclePrefabs[vehicleIndex].transform.rotation);
    }

    void SpawnCoin()
    {
        float offsetX = Random.Range(-coinSpanwRangeX, coinSpanwRangeX);
        offsetZCoin += Random.Range(coinSpanwRangeZ * 10, coinSpanwRangeZ * 35);
        Vector3 offset = new Vector3(offsetX, 2, offsetZCoin);
        Instantiate(coinPrefab, playerTransform.position + offset, coinPrefab.transform.rotation);
    }
}
