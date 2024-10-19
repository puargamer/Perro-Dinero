using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaterialType
{
    Red,
    Blue,
    Yellow
}

public class SpawnerManager : MonoBehaviour
{
    public GameObject spawnerPrefab;
    public int initialSpawnerCount = 5;
    public float newSpawnerDelay = 10f;

    public float spawnRangeX = 10f;
    public float spawnYValue = 0f;
    public float spawnRangeZ = 10f;

    private int materialTypeCount;

    // Start is called before the first frame update
    void Start()
    {
        materialTypeCount = System.Enum.GetNames(typeof(MaterialType)).Length;
        SpawnInitialSpawners();
    }

    private void SpawnInitialSpawners()
    {
        for (int i = 0; i < initialSpawnerCount; i++)
        {
            Vector3 randomPos = GetRandomSpawnPosition();

            GameObject spawner = Instantiate(spawnerPrefab, randomPos, Quaternion.identity);

            // guaranteed first 3 material spawn if possible
            // spawn random material for remaining
            MaterialType typeToSpawn = (i < materialTypeCount) ? (MaterialType)i :
                                        (MaterialType)Random.Range(0, materialTypeCount);

            MaterialSpawner spawnerScript = spawner.GetComponent<MaterialSpawner>();
            spawnerScript.SetupSpawner(typeToSpawn);
        }
    }

    public void SpawnerDestroyed()
    {
        StartCoroutine(SpawnNewSpawnerAfterDelay());
    }

    private IEnumerator SpawnNewSpawnerAfterDelay()
    {
        yield return new WaitForSeconds(newSpawnerDelay);

        Vector3 randomPos = GetRandomSpawnPosition();

        GameObject newSpawner = Instantiate(spawnerPrefab, randomPos, Quaternion.identity);

        // choose random material
        MaterialType randomType = (MaterialType)Random.Range(0, materialTypeCount);

        MaterialSpawner newSpawnerScript = newSpawner.GetComponent<MaterialSpawner>();
        newSpawnerScript.SetupSpawner(randomType);
    }

    //private Vector3 checkForValidSpawn()
    //{
    //    return null;
    //}

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(
            Random.Range(-spawnRangeX, spawnRangeX),
            spawnYValue,
            Random.Range(-spawnRangeZ, spawnRangeZ)
        );
    }
}
