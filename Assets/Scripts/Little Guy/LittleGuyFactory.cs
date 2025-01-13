using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LittleGuyFactory : MonoBehaviour
{
    public GameObject littleGuyPrefab;

    public static LittleGuyFactory Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject CreateLittleGuy(Vector3 position, CombinationType combinationType)
    {
        GameObject littleGuy = Instantiate(littleGuyPrefab, position, Quaternion.identity);
        StartCoroutine(InstantiateLittleGuy(littleGuy, combinationType)); // need to do this because of sprite bug
        return littleGuy;
    }

    private IEnumerator InstantiateLittleGuy(GameObject littleGuy, CombinationType combinationType)
    {
        yield return new WaitForEndOfFrame();
        LittleGuyNav littleGuyNav = littleGuy.GetComponent<LittleGuyNav>();
        littleGuyNav.Setup(combinationType);
    }
}

/* 
    Example Usage:
    public LittleGuyFactory littleGuyFactory;
    
    littleGuyFactory.CreateLittleGuy(spawnPosition, materialType);
 
 */