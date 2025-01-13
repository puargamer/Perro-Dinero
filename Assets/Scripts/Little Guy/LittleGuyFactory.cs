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
        StartCoroutine(InstantiateLittleGuy(littleGuy, combinationType));
        return littleGuy;
    }

    // need to do this because of a sprite bug
    private IEnumerator InstantiateLittleGuy(GameObject littleGuy, CombinationType combinationType)
    {
        yield return new WaitForEndOfFrame();
        LittleGuyNav littleGuyNav = littleGuy.GetComponent<LittleGuyNav>();
        littleGuyNav.Setup(combinationType);
    }

    public GameObject LoadLittleGuy(LittleGuyData data)
    {
        Debug.Log($"running LoadLittleGuy, instantiating {(int)data.combinationType} lure");
        GameObject newGuy = Instantiate(littleGuyPrefab, new Vector3(70f, 3.2f, 51f), Quaternion.identity); // hard coded, please change later
        StartCoroutine(LoadInstantiateLittleGuy(newGuy, data));
        //newGuy.GetComponent<LittleGuyNav>().SetLittleGuyData(data);
        return newGuy;
    }

    private IEnumerator LoadInstantiateLittleGuy(GameObject newGuy, LittleGuyData data)
    {
        yield return new WaitForEndOfFrame();
        newGuy.GetComponent<LittleGuyNav>().SetLittleGuyData(data);
    }

}

/* 
    Example Usage:
    public LittleGuyFactory littleGuyFactory;
    
    littleGuyFactory.CreateLittleGuy(spawnPosition, materialType);
 
 */