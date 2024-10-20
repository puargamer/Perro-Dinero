using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float xOffset = 0f;
    public float spawnRangeZ = 15f;
    public float zOffset = 0f;

    [Header("Exclude Ground")]
    public LayerMask validSpawnLayerMask;
    private int materialTypeCount;
    private List<Transform> activeSpawners = new List<Transform>(); // keep track of all spawners

    [SerializeField] // for debugging
    private int[] materialLikelihood;
    // Start is called before the first frame update
    void Start()
    {
        materialTypeCount = MaterialTypeHelper.Count;
        materialLikelihood = new int[materialTypeCount]; // init to 0s
        SpawnInitialSpawners();
    }

    private void SpawnInitialSpawners()
    {
        for (int i = 0; i < initialSpawnerCount; i++)
        {
            Vector3 randomPos = GetValidRandomSpawnPosition();
            GameObject spawner = Instantiate(spawnerPrefab, randomPos, Quaternion.identity);
            activeSpawners.Add(spawner.transform);

            // guaranteed first 3 material spawn if possible
            // spawn random material for remaining
            MaterialType typeToSpawn = (i < materialTypeCount) ? (MaterialType)i : GetRandomMaterialType();

            MaterialSpawner spawnerScript = spawner.GetComponent<MaterialSpawner>();
            spawnerScript.SetupSpawner(typeToSpawn);
            UpdateMaterialLikelihood((int)typeToSpawn);
        }
    }

    public void SpawnerDestroyed(GameObject gameObject)
    {
        activeSpawners.Remove(gameObject.transform);
        StartCoroutine(SpawnNewSpawnerAfterDelay());
    }

    private IEnumerator SpawnNewSpawnerAfterDelay()
    {
        yield return new WaitForSeconds(newSpawnerDelay);

        Vector3 randomPos = GetValidRandomSpawnPosition();
        GameObject newSpawner = Instantiate(spawnerPrefab, randomPos, Quaternion.identity);
        activeSpawners.Add(newSpawner.transform);

        // modified choose random material
        MaterialType typeToSpawn = GetRandomMaterialType();

        MaterialSpawner newSpawnerScript = newSpawner.GetComponent<MaterialSpawner>();
        newSpawnerScript.SetupSpawner(typeToSpawn);
        UpdateMaterialLikelihood((int)typeToSpawn);
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
        foreach (Transform spawner in activeSpawners) // if it will spawn near another spawner
        { // temporary trash code, too many calculations if high spawner count
            if (Vector3.Distance(position, spawner.position) < minSpawnerGap)
            {
                return false; // too close
            }
        }

        Collider[] colliders = Physics.OverlapSphere(position, 0.5f, validSpawnLayerMask);
        return colliders.Length == 0; // no colliders overlapping the position
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(
            Random.Range(-spawnRangeX, spawnRangeX) + xOffset,
            spawnYValue,
            Random.Range(-spawnRangeZ, spawnRangeZ) + zOffset
        );
    }

    private MaterialType GetRandomMaterialType()
    {
        int totalLikelihood = 0;

        foreach (var likelihood in materialLikelihood)
        {
            totalLikelihood += likelihood;
        }

        // every one has equal chance
        if (totalLikelihood == 0) 
        {
            return (MaterialType)Random.Range(0, materialTypeCount);
        }

        // if others have weighted spawns
        int randomValue = Random.Range(0, totalLikelihood);
        int accumulated = 0;

        for (int i = 0; i < materialTypeCount; i++)
        {
            accumulated += materialLikelihood[i];
            if (randomValue < accumulated)
            {
                return (MaterialType)i;
            }
        }

        return (MaterialType)0;
    }

    private void UpdateMaterialLikelihood(int spawnedIndex)
    {
        for (int i = 0; i < materialTypeCount; i++)
        {
            if (i == spawnedIndex)
            {
                materialLikelihood[i] = Mathf.Max(0, materialLikelihood[i] - 2);
            }
            else
            {
                materialLikelihood[i] += 1;
            }
        }
    }
}
