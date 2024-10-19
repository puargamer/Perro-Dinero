using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// spawninitspawners and materialtype should be modified as we progress
// garbage code rn
public enum MaterialType 
{
    Red,
    Blue,
    Yellow
}

public class SpawnerManager : MonoBehaviour
{
    public GameObject spawnerPrefab;
    [Header("Spawn Settings")]
    public int initialSpawnerCount = 5;
    public float newSpawnerDelay = 10f;
    public float minSpawnerGap = 15f;
    public float spawnYValue = 0f;
    [Header("Spawn Radius")]
    public float spawnRangeX = 10f;
    public float spawnRangeZ = 10f;

    private int materialTypeCount;
    private List<Transform> spawnerTransforms = new List<Transform>(); // keep track of all spawners

    // Start is called before the first frame update
    void Start()
    {
        materialTypeCount = System.Enum.GetNames(typeof(MaterialType)).Length; // change this garbage
        SpawnInitialSpawners();
    }

    private void SpawnInitialSpawners()
    {
        for (int i = 0; i < initialSpawnerCount; i++)
        {
            Vector3 randomPos = GetValidRandomSpawnPosition();
            GameObject spawner = Instantiate(spawnerPrefab, randomPos, Quaternion.identity);
            spawnerTransforms.Add(spawner.transform);

            // guaranteed first 3 material spawn if possible
            // spawn random material for remaining
            MaterialType typeToSpawn = (i < materialTypeCount) ? (MaterialType)i :
                                        (MaterialType)Random.Range(0, materialTypeCount);

            MaterialSpawner spawnerScript = spawner.GetComponent<MaterialSpawner>();
            spawnerScript.SetupSpawner(typeToSpawn);
        }
    }

    public void SpawnerDestroyed(GameObject gameObject)
    {
        spawnerTransforms.Remove(gameObject.transform);
        StartCoroutine(SpawnNewSpawnerAfterDelay());
    }

    private IEnumerator SpawnNewSpawnerAfterDelay()
    {
        yield return new WaitForSeconds(newSpawnerDelay);

        Vector3 randomPos = GetValidRandomSpawnPosition();
        GameObject newSpawner = Instantiate(spawnerPrefab, randomPos, Quaternion.identity);
        spawnerTransforms.Add(newSpawner.transform);

        // choose random material
        MaterialType randomType = (MaterialType)Random.Range(0, materialTypeCount);

        MaterialSpawner newSpawnerScript = newSpawner.GetComponent<MaterialSpawner>();
        newSpawnerScript.SetupSpawner(randomType);
    }

    private Vector3 GetValidRandomSpawnPosition()
    {
        Vector3 randomPos;
        do
        {
            randomPos = GetRandomSpawnPosition();
        } while (!IsValidSpawnPosition(randomPos));

        return randomPos;
    }

    private bool IsValidSpawnPosition(Vector3 position)
    {
        foreach (Transform spawnerTransform in spawnerTransforms)
        { // temporary trash code, too many calculations if high spawner count
            if (Vector3.Distance(position, spawnerTransform.position) < minSpawnerGap)
            {
                return false; // too close
            }
        }
        return true;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(
            Random.Range(-spawnRangeX, spawnRangeX),
            spawnYValue,
            Random.Range(-spawnRangeZ, spawnRangeZ)
        );
    }
}
