using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuySpawner : MonoBehaviour
{
    public LittleGuyFactory littleGuyFactory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //littleGuyFactory.CreateLittleGuy(new Vector3(0f, 1.5f, 0f), (MaterialType)0);
            // nuked for now
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
