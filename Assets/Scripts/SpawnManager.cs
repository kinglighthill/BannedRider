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

    private float vehicleSpawnRangeX = 1f;
    private float vehicleSpawnPosZ = 50.0f;
    private int vehicleMaxSpawn = 5;
    private float vehicleSpawnRate = 10f;

    private float coinSpanwRangeX = 5f;
    private float coinSpanwRangeZ = 1f;
    private float coinSpawnPosZ = 25f;
    private int coinMaxSpawn = 20;
    private float coinSpawnRate = 5f;

    private int count = 0;

    //GameManager gameManager;

    private float[] vehicleXPostions = new float[4] { 3f, 8f, 14f, 19f};
    private float[] coinXPostions = new float[6] {4f, 6.5f, 9f, 11.5f, 14f, 16.5f};

    // Start is called before the first frame update
    void Start()
    {
        offsetZVehicle = offsetZCoin = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnVehicle()
    {
        int vehicleIndex = Random.Range(0, vehiclePrefabs.Length);

        bool isIncoming = Random.Range(0f, 1f) > 0.5;
        float xPosition = isIncoming ? vehicleXPostions[Random.Range(0, 2)] : vehicleXPostions[Random.Range(2, 4)];
        Quaternion rotation = isIncoming ? new Quaternion(0f, 180f, 0, 0f) : vehiclePrefabs[vehicleIndex].transform.rotation;

        offsetZVehicle += vehicleSpawnPosZ;
        float zPosition = playerTransform.position.z + offsetZVehicle;
        Vector3 spawnPos = new Vector3(xPosition, -14.75f, zPosition);

        Vehicle vehicle = Instantiate(vehiclePrefabs[vehicleIndex], spawnPos, rotation).GetComponent<Vehicle>();
        vehicle.IsIncoming = isIncoming;
    }

    void SpawnCoin()
    {
        float xPosition = coinXPostions[Random.Range(0, coinXPostions.Length)];
        offsetZCoin += coinSpawnPosZ;
        float zPosition = playerTransform.position.z + offsetZCoin;

        //float offsetX = Random.Range(-coinSpanwRangeX, coinSpanwRangeX);
        //offsetZCoin += Random.Range(coinSpanwRangeZ * 10, coinSpanwRangeZ * 35);

        Vector3 spawnPos = new Vector3(xPosition, playerTransform.position.y + 2, zPosition);
        Instantiate(coinPrefab, spawnPos, coinPrefab.transform.rotation);
    }

    void BatchSpawnVehicle()
    {
        int numberToSpawn = Random.Range(2, vehicleMaxSpawn);

        for (int i = 0; i < numberToSpawn; i++)
        {
            SpawnVehicle();
        }
    }

    public void StartSpawn()
    {
        InvokeRepeating("BatchSpawnVehicle", 0, vehicleSpawnRate);
        InvokeRepeating("SpawnCoin", 0, coinSpawnRate);
    }
}
