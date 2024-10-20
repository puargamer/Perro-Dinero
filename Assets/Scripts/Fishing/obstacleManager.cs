using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleManager : MonoBehaviour
{
    public int spawnCount;
    public GameObject logPrefab;
    // Start is called before the first frame update
    void Start()
    {
        spawnCount = Random.Range(1, 3);
        InitialLogSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitialLogSpawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(-87, -67), 0, Random.Range(-33, 39));
            Instantiate(logPrefab, randPos, Quaternion.identity);
        }
    }
}
