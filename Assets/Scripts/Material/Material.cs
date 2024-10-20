using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : ColorUtility
{
    public MaterialType materialType; // set an enum
    [SerializeField] private MaterialSpawner spawner;

    private SpriteRenderer spriteRenderer;
    private Renderer materialRenderer;

    public void Setup(MaterialSpawner spawner, MaterialType type, Sprite sprite)
    {
        this.spawner = spawner;
        materialType = type;
        materialRenderer = GetComponent<Renderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetColor(materialRenderer, materialType);
        spriteRenderer.sprite = sprite;
    }

    void OnTriggerEnter(Collider other)
    {
        // add code here to tell the player that they have collected this material!
        if (other.CompareTag("Player"))
        {
            // uncomment this when singleton is ready!
            //Singleton.Instance.CollectMat((int)materialType); 
            spawner.CollectMaterial();
            Destroy(gameObject);
            // collected!
        }
    }
}
