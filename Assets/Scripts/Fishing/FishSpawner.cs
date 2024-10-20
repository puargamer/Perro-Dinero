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
        //new movement locations
        //x: -93 to -60
        //z: -27 to 39

        //initial spawns
        //x: -7 to 15
        //z: 52 to 67
        //new initial spawns
        //x: -67 to -53
        //z: -9 to 16
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
            Vector3 randPos = new Vector3(Random.Range(-67, -53), 0, Random.Range(-9, 16));
            Instantiate(fishPrefab, randPos, Quaternion.identity);
        }
    }
}
