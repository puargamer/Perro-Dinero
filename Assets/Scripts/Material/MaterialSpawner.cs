using UnityEngine;

public class MaterialSpawner : MonoBehaviour
{
    public GameObject materialPrefab;
    private SpawnerManager spawnerManager;
    public MaterialType currMatType;

    private int numberOfSpawns = 3;
    public float spawnRadius = 5f;
    public float respawnDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        spawnerManager = FindObjectOfType<SpawnerManager>();
        SpawnMaterials();
    }

    public void SetupSpawner(MaterialType type)
    {
        currMatType = type;
    }

    private void SpawnMaterials()
    {
        // find a random position within the range and spawn the material
        for (int i = 0; i < numberOfSpawns; i++)
        {
            Vector3 randomPos = GetRandomSpawnPosition();
            GameObject newMat = Instantiate(materialPrefab, randomPos, Quaternion.identity);
            Material matScript = newMat.GetComponent<Material>();
            matScript.Setup(this, currMatType);
        }
    }

    public void CollectMaterial() // somehow call this when 
    {
        numberOfSpawns--;

        if (numberOfSpawns <= 0)
        {
            DestroySpawner();
        }
    }

    void DestroySpawner()
    {
        // tell the manager that its been destroyed
        spawnerManager.SpawnerDestroyed();
        Destroy(gameObject);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(
            transform.position.x + Random.Range(-spawnRadius, spawnRadius),
            transform.position.y + 1f, // temporarily above floor
            transform.position.z + Random.Range(-spawnRadius, spawnRadius)
        );
    }
}
