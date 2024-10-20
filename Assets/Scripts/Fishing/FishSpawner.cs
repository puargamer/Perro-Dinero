using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public int spawnCount;
    public GameObject fishPrefab;

    // Start is called before the first frame update
    void Start()
    {
        spawnCount = Random.Range(2, 5);
        //movement locations
        //x: -29 to 42
        //z: 60 to 92

        //initial spawns
        //x: -7 to 15
        //z: 52 to 67
        InitalFishSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitalFishSpawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(-7, 15), 0, Random.Range(52, 67));
            Instantiate(fishPrefab, randPos, Quaternion.identity);
        }
    }
}
