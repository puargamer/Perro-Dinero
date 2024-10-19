using UnityEngine;

public class MaterialSpawner : MonoBehaviour
{
    public GameObject materialPrefab;
    private SpawnerManager spawnerManager;

    private int numberOfSpawns = 3;
    public float spawnRadius = 5f;
    public float respawnDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        spawnerManager = FindObjectOfType<SpawnerManager>();
        SpawnMaterials();
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    void SpawnMaterials()
    {
        for (int i = 0; i < numberOfSpawns; i++)
        { // find a random position within the range and spawn the material
            Vector3 randomPos = new Vector3(
                transform.position.x + Random.Range(-spawnRadius, spawnRadius),
                transform.position.y + 1f,
                transform.position.z + Random.Range(-spawnRadius, spawnRadius)
            );
            Instantiate(materialPrefab, randomPos, Quaternion.identity);
        }
    }

    public void CollectMaterial()
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
        Destroy(this);
        //return;
    }
}
