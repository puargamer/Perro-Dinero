using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour
{
    public MaterialType materialType; // set an enum
    [SerializeField] private MaterialSpawner spawner;

    private Renderer materialRenderer;

    public void Setup(MaterialSpawner spawner, MaterialType type)
    {
        this.spawner = spawner;
        materialType = type;
        materialRenderer = GetComponent<Renderer>();
        SetMaterialColor();
    }

    private void SetMaterialColor()
    {
        Color wantedColor;

        switch (materialType)
        {
            case MaterialType.Red:
                wantedColor = Color.red;
                break;
            case MaterialType.Blue:
                wantedColor = Color.blue;
                break;
            case MaterialType.Yellow:
                wantedColor = Color.yellow;
                break;
            default:
                wantedColor = Color.white;
                break;
        }

        materialRenderer.material.color = wantedColor;
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
