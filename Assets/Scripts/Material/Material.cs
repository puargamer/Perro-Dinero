using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour
{
    public MaterialType materialType; // set an enum
    [SerializeField] private MaterialSpawner spawner;

    public void Setup(MaterialSpawner spawner, MaterialType type)
    {
        this.spawner = spawner;
        materialType = type;
    }

    void OnTriggerEnter(Collider other)
    {
        spawner.CollectMaterial();

        // add code here to tell the player that they have collected this material!
        //if (other.CompareTag("Player"))
        //{
        //    Player player = other.GetComponent<Player>();
        //    // collected!
        //}
            Destroy(gameObject);
    }
}
