using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawnerManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnArea
    {
        public Transform SpawnCenter;
        public float SpawnRange = 5f;
        public MaterialType MaterialType;
        public int MaxSpawns = 5;

        public SpawnArea(Transform spawnCenter, float spawnRange, MaterialType materialType, int maxSpawns)
        {
            SpawnCenter = spawnCenter;
            SpawnRange = spawnRange;
            MaterialType = materialType;
            MaxSpawns = maxSpawns;
        }
    }

    [Header("Spawn Areas")]
    public SpawnArea[] spawnAreas = new SpawnArea[5];

    public float spawnYValue = 0f;

    public GameObject spawnerPrefab;
    public float newSpawnerDelay = 5f;
    private int materialTypeCount;

    void Start()
    {
        materialTypeCount = MaterialTypeHelper.Count;

        foreach (var spawnArea in spawnAreas)
        {
            SpawnSpawner(spawnArea);
        }
    }

    private void SpawnSpawner(SpawnArea spawnArea)
    {
        Vector3 spawnPosition = spawnArea.SpawnCenter.position;

        for (int i = 0; i < spawnArea.MaxSpawns; i++)
        {
            Vector3 randomPosition = spawnPosition + GetRandomOffset(spawnArea.SpawnRange);
            SpawnSpawner(spawnArea.MaterialType, randomPosition);
        }
    }

    private void SpawnSpawner(MaterialType materialType, Vector3 position)
    {
        GameObject newSpawner = Instantiate(spawnerPrefab, position, Quaternion.identity);
        MaterialSpawner spawnerScript = newSpawner.GetComponent<MaterialSpawner>();
        spawnerScript.SetupSpawner(materialType);
    }

    private Vector3 GetRandomOffset(float range)
    {
        float xOffset = Random.Range(-range, range);
        float zOffset = Random.Range(-range, range);
        return new Vector3(xOffset, spawnYValue, zOffset);
    }

    public void SpawnerDestroyed(GameObject gameObject)
    {
        MaterialSpawner spawnerScript = gameObject.GetComponent<MaterialSpawner>();
        StartCoroutine(SpawnNewSpawnerAfterDelay(spawnerScript.currMatType));
    }

    private IEnumerator SpawnNewSpawnerAfterDelay(MaterialType materialType)
    {
        yield return new WaitForSeconds(newSpawnerDelay);

        foreach (var spawnArea in spawnAreas)
        {
            if (spawnArea.MaterialType == materialType)
            {
                Vector3 spawnPosition = spawnArea.SpawnCenter.position;
                Vector3 randomPosition = spawnPosition + GetRandomOffset(spawnArea.SpawnRange);
                SpawnSpawner(materialType, randomPosition);
                break;
            }
        }
    }
}
