using UnityEngine;

public class MaterialSpawner : MonoBehaviour
{
    public GameObject materialPrefab;
    private SpawnerManager spawnerManager;
    public MaterialType currMatType;

    [Header("Spawn Options")]
    private int numberOfSpawns = 3;
    public float spawnRadius = 5f;
    public float respawnDelay = 2f;
    [Header("Exclude Ground")]
    public LayerMask validSpawnLayerMask;

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
            Vector3 randomPos;
            int attempts = 0;
            do
            {
                randomPos = GetRandomSpawnPosition();
                attempts++;
                if (attempts >= 150)
                {
                    Debug.Log("you screwed up but in MaterialSpawner.cs");
                    return;
                }
            } while (!IsValidSpawnPosition(randomPos));


            GameObject newMat = Instantiate(materialPrefab, randomPos, Quaternion.identity);
            Material matScript = newMat.GetComponent<Material>();
            matScript.Setup(this, currMatType);
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
        spawnerManager.SpawnerDestroyed(gameObject);
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

    private bool IsValidSpawnPosition(Vector3 position) // do not spawn in a tree/wall check
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.5f, validSpawnLayerMask);
        return colliders.Length == 0;
    }
}
