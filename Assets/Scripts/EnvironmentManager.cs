using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject environment;
    public List<GameObject> activeEnvironments = new List<GameObject>();
    public float zSpawn = 0;
    public float length;
    public int numberOfEnvironments = 2;

    public Transform playerTransform;

    public float x;
    public float z;

    // Start is called before the first frame update
    void Start()
    {
        //length = environment.GetComponent<BoxCollider>().size.x;
        length = 190;
        SpawnEnvironment();
        for (int i =0; i < numberOfEnvironments; i++)
        {
            SpawnEnvironment();
        }

        x = playerTransform.position.x;
        z = zSpawn - (numberOfEnvironments * length);
    }

    // Update is called once per frame
    void Update()
    {
        x = playerTransform.position.x;
        z = zSpawn - (numberOfEnvironments * length);

        if (Mathf.Abs(playerTransform.position.x) > (zSpawn - (numberOfEnvironments * length)))
        {
            SpawnEnvironment();
            DeleteEnvironment();
        }
    }

    public void SpawnEnvironment()
    {
        GameObject gameObject = Instantiate(environment, -(transform.right * zSpawn), transform.rotation);
        activeEnvironments.Add(gameObject);
        zSpawn += length; 
    }

    private void DeleteEnvironment()
    {
        Destroy(activeEnvironments[0]);
        activeEnvironments.RemoveAt(0);
    }
}