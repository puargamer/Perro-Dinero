using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject spawnerPrefab;
    public int initialSpawnerCount = 5;
    public float newSpawnerDelay = 10f;

    public float spawnRangeX = 10f;
    public float spawnYValue = 0f;
    public float spawnRangeZ = 10f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnInitialSpawners();
    }

    private void SpawnInitialSpawners()
    {
        for (int i = 0; i < initialSpawnerCount; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-spawnRangeX, spawnRangeX),
                0,
                Random.Range(-spawnRangeZ, spawnRangeZ)
            );

            Instantiate(spawnerPrefab, randomPos, Quaternion.identity);
        }
    }

    public void SpawnerDestroyed()
    {
        StartCoroutine(SpawnNewSpawnerAfterDelay());
    }

    private IEnumerator SpawnNewSpawnerAfterDelay()
    {
        yield return new WaitForSeconds(newSpawnerDelay);
        
        Vector3 randomPos = new Vector3(
            Random.Range(-spawnRangeX, spawnRangeX),
            spawnYValue, 
            Random.Range(-spawnRangeZ, spawnRangeZ)
        );

        Instantiate(spawnerPrefab, randomPos, Quaternion.identity);
    }
}
