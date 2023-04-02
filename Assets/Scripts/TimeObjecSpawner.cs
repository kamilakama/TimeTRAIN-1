using UnityEngine;

public class TimeObjectSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;   // Array of object prefabs to be spawned
    public int numObjectsToSpawn = 1;    // Number of objects to be spawned
    public float spawnInterval = 5f;     // Time interval between spawning objects
    public float objectLifespan = 10f;   // Time until objects despawn
    public BoxCollider spawnArea;        // Spawn area for objects

    private float timer = 0f;           // Timer to keep track of when to spawn objects
    private Vector3[] occupiedPositions; // Array of positions where objects have already spawned

    void Start()
    {
        occupiedPositions = new Vector3[numObjectsToSpawn];
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Check if enough time has passed to spawn new objects
        if (timer >= spawnInterval)
        {
            // Spawn the specified number of objects
            for (int i = 0; i < numObjectsToSpawn; i++)
            {
                Vector3 randomPosition = GetRandomPositionInSpawnArea();
                int randomIndex = Random.Range(0, objectPrefabs.Length);
                GameObject newObject = Instantiate(objectPrefabs[randomIndex], randomPosition, transform.rotation);
                BoxCollider newCollider = newObject.AddComponent<BoxCollider>();
                Destroy(newObject, objectLifespan);
                occupiedPositions[i] = newObject.transform.position;
            }

            // Reset timer
            timer = 0f;
        }
    }

    // Generate a random position within the spawn area that is not already occupied
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