using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LittleGuyFactory : MonoBehaviour
{
    public GameObject littleGuyPrefab;
    
    public GameObject CreateLittleGuy(Vector3 position, MaterialType materialType)
    {
        GameObject littleGuy = Instantiate(littleGuyPrefab, position, Quaternion.identity);
        StartCoroutine(InstantiateLittleGuy(littleGuy, materialType));
        return littleGuy;
    }

    private IEnumerator InstantiateLittleGuy(GameObject littleGuy, MaterialType materialType)
    {
        yield return new WaitForEndOfFrame();
        LittleGuyNav littleGuyNav = littleGuy.GetComponent<LittleGuyNav>();
        littleGuyNav.Setup(materialType);
    }
}

/* 
    Example Usage:
    public LittleGuyFactory littleGuyFactory;
    
    littleGuyFactory.CreateLittleGuy(spawnPosition, materialType);
 
 */