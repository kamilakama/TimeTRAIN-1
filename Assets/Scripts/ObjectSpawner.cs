using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour 
{
    public GameObject[] objectPrefabs;
    public int numObjectsToSpawn = 1;
    public float spawnDelay = 5f;
    public float objectLifespan = 10f;
    public BoxCollider spawnArea;

    private float timer = 0f;
    private Vector3[] occupiedPositions;

    void Start()
    {
        occupiedPositions = new Vector3[numObjectsToSpawn];
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnDelay)
        {
            for (int i = 0; i < numObjectsToSpawn; i++)
            {
                Vector3 randomPosition = GetRandomPositionInSpawnArea();
                int randomIndex = Random.Range(0, objectPrefabs.Length);
                GameObject newObject = Instantiate(objectPrefabs[randomIndex], randomPosition, transform.rotation);
                BoxCollider newCollider = newObject.AddComponent<BoxCollider>();
                Destroy(newObject, objectLifespan);
                occupiedPositions[i] = newObject.transform.position;
            }
            timer = 0f;
        }
    }

    private Vector3 GetRandomPositionInSpawnArea()
    {
        Vector3 randomPosition = Vector3.zero;
        if (spawnArea != null)
        {
            bool positionOccupied = true;
            int maxAttempts = 10;
            int attemptCount = 0;

            while (positionOccupied && attemptCount < maxAttempts)
            {
                randomPosition = spawnArea.transform.position + new Vector3(
                    Random.Range(-spawnArea.size.x / 2f, spawnArea.size.x / 2f),
                    Random.Range(-spawnArea.size.y / 2f, spawnArea.size.y / 2f),
                    Random.Range(-spawnArea.size.z / 2f, spawnArea.size.z / 2f)
                );

                positionOccupied = false;
                for (int i = 0; i < occupiedPositions.Length; i++)
                {
                    if (occupiedPositions[i] != Vector3.zero && Vector3.Distance(randomPosition, occupiedPositions[i]) < 1f)
                    {
                        positionOccupied = true;
                        break;
                    }
                }

                attemptCount++;
            }
        }
        return randomPosition;
    }
}
