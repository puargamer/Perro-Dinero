using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LittleGuyFactory : MonoBehaviour
{
    public GameObject littleGuyPrefab;
    
    public GameObject CreateLittleGuy(Vector3 position, MaterialType materialType)
    {
        GameObject littleGuy = Instantiate(littleGuyPrefab, position, Quaternion.identity);
        littleGuy.GetComponent<NavMeshAgent>().enabled = false;
        StartCoroutine(NVMAgentActivate(littleGuy, materialType));
        return littleGuy;
    }

    private IEnumerator NVMAgentActivate(GameObject littleGuy, MaterialType materialType)
    {
        yield return new WaitForEndOfFrame();
        littleGuy.GetComponent<NavMeshAgent>().enabled = true;
        LittleGuyNav littleGuyNav = littleGuy.GetComponent<LittleGuyNav>();
        littleGuyNav.Setup(materialType);
    }
}

/* 
    Example Usage:
    public LittleGuyFactory littleGuyFactory;
    
    littleGuyFactory.CreateLittleGuy(spawnPosition, materialType);
 
 */