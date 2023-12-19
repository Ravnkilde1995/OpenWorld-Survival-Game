using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject[] myObjects;
    //public GameObject healthBar;
    public int numberOfObjectsToSpawn = 50;

    void Start()
    {
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            SpawnRandomObject(myObjects);
        }
    }

    void SpawnRandomObject(GameObject[] objectsToSpawn)
    {
        if (objectsToSpawn.Length == 0)
        {
            Debug.LogError("No objects to spawn. Please assign objects to the array.");
            return;
        }

        int randomIndex = Random.Range(0, objectsToSpawn.Length);
        Vector3 randomSpawnPosition = new Vector3(Random.Range(20, 480), 41, Random.Range(20, 480));

        GameObject spawnedObject = Instantiate(objectsToSpawn[randomIndex], randomSpawnPosition, Quaternion.identity);
        //GameObject spawnedHealthBar = Instantiate(healthBar, randomSpawnPosition, Quaternion.identity);

        // Set the health bar for the spawned object
        //spawnedObject.GetComponent<RabbitHealth>().healthBar = spawnedHealthBar.GetComponent<RabbitHealthBar>();

    }


}