using UnityEngine;

public class LittleGuyFactory : MonoBehaviour
{
    public GameObject littleGuyPrefab;
    
    public GameObject CreateLittleGuy(Vector3 position, MaterialType materialType)
    {
        GameObject littleGuy = Instantiate(littleGuyPrefab, position, Quaternion.identity);
        LittleGuyNav littleGuyNav = littleGuy.GetComponent<LittleGuyNav>();
        littleGuyNav.Setup(materialType);
        return littleGuy;
    }
}

/* 
    Example Usage:
    public LittleGuyFactory littleGuyFactory;
    
    littleGuyFactory.CreateLittleGuy(spawnPosition, materialType);
 
 */