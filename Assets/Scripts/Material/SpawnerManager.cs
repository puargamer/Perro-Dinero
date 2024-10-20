using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spawninitspawners and materialtype should be modified as we progress
// garbage code rn


public class SpawnerManager : MonoBehaviour
{
    public GameObject spawnerPrefab;
    [Header("Spawn Settings")]
    public int initialSpawnerCount = 5;
    public float newSpawnerDelay = 10f;
    public float minSpawnerGap = 2f;
    public float spawnYValue = 0f;
    [Header("Spawn Radius")]
    public float spawnRangeX = 15f;
    public float spawnRangeZ = 15f;
    [Header("Exclude Ground")]
    public LayerMask validSpawnLayerMask;
    private int materialTypeCount;
    private List<Transform> activeSpawnerTransforms = new List<Transform>(); // keep track of all spawners

    private int[] materialLikelihood = new int[3] {0, 0, 0};
    // Start is called before the first frame update
    void Start()
    {
        materialTypeCount = 3; // first 3 colors base
        SpawnInitialSpawners();
    }

    private void SpawnInitialSpawners()
    {
        for (int i = 0; i < initialSpawnerCount; i++)
        {
            Vector3 randomPos = GetValidRandomSpawnPosition();
            GameObject spawner = Instantiate(spawnerPrefab, randomPos, Quaternion.identity);
            activeSpawnerTransforms.Add(spawner.transform);

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
        activeSpawnerTransforms.Remove(gameObject.transform);
        StartCoroutine(SpawnNewSpawnerAfterDelay());
    }

    private IEnumerator SpawnNewSpawnerAfterDelay()
    {
        yield return new WaitForSeconds(newSpawnerDelay);

        Vector3 randomPos = GetValidRandomSpawnPosition();
        GameObject newSpawner = Instantiate(spawnerPrefab, randomPos, Quaternion.identity);
        activeSpawnerTransforms.Add(newSpawner.transform);

        // modified choose random material
        MaterialType randomType = (MaterialType)Random.Range(0, materialTypeCount);


        MaterialSpawner newSpawnerScript = newSpawner.GetComponent<MaterialSpawner>();
        newSpawnerScript.SetupSpawner(randomType);
    }

    private Vector3 GetValidRandomSpawnPosition()
    {
        Vector3 randomPos;
        int attempts = 0;
        do
        {
            randomPos = GetRandomSpawnPosition();
            attempts++;
            if (attempts >= 150)
            {
                Debug.Log("Failed to find spawn location: lower the min spawner gap in inspector" +
                    " or this is colliding w the ground");
                break;
            }
        } while (!IsValidSpawnPosition(randomPos));

        return randomPos;
    }

    private bool IsValidSpawnPosition(Vector3 position)
    {
        foreach (Transform spawnerTransform in activeSpawnerTransforms) // if it will spawn near another spawner
        { // temporary trash code, too many calculations if high spawner count
            if (Vector3.Distance(position, spawnerTransform.position) < minSpawnerGap)
            {
                return false; // too close
            }
        }

        Collider[] colliders = Physics.OverlapSphere(position, 0.5f, validSpawnLayerMask);
        return colliders.Length == 0; // no colliders overlapping the position
        //return true;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(
            Random.Range(-spawnRangeX, spawnRangeX),
            spawnYValue,
            Random.Range(-spawnRangeZ, spawnRangeZ)
        );
    }

    //private modifyLikelihood(int index)
    //{
    //    materialLikelihood
    //}
}
