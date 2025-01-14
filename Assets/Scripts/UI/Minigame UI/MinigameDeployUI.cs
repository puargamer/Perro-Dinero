using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// very hard coded class, i hope we can replace this next time
public class MinigameDeployUI : MonoBehaviour
{
    public GameObject gridView;
    // public GameObject playerCam;
    public GameObject playerCamera;
    public GameObject minigameDeployUI;
    public Transform fishSpawnLocation;

    public GameObject dapMinigamePrefab;
    public GameObject deployIconPrefab;
    private GameObject currentMinigame;

    private Sprite selectionSprite;
    private CombinationType selectionCombType;

    // Start is called before the first frame update
    void Start()
    {
        dapCheck.OnGameWon += SpawnFishReward;
        // mainCamera = Camera.main.gameObject;
    }

    private void OnDestroy()
    {
        dapCheck.OnGameWon -= SpawnFishReward;
    }

    public void AddToGrid(Sprite icon, GameObject littleGuy) /// little guy probably not needed
    {
        GameObject newIcon = Instantiate(deployIconPrefab, Vector3.zero, Quaternion.identity, gridView.transform);
        Image lureImage = newIcon.GetComponentInChildren<Image>();

        lureImage.sprite = icon;
        lureImage.preserveAspect = true;

        Button iconButton = newIcon.GetComponent<Button>();
        iconButton.onClick.AddListener(() => saveIcon(icon, littleGuy));
    }

    private void saveIcon(Sprite icon, GameObject guy)
    {
        // selectionSprite = itself.GetComponent<Image>().sprite;
        selectionSprite = icon;
        selectionCombType = guy.GetComponent<LittleGuyNav>().combinationType; // need to know what fish
    }

    public void Deploy()
    {
        CloseUI();
        GameObject.Find("Player").GetComponent<PlayerInteract>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        currentMinigame = Instantiate(dapMinigamePrefab);
        // set the little guy sprite in the prefab
        foreach (Transform child in currentMinigame.transform)
        {
            if (child.name == "dapMinigameStart")
            {
                Transform summon = child.transform.Find("dapUpLittleGuy");
                summon.GetComponentInChildren<SpriteRenderer>().sprite = selectionSprite;
                break;
            }
            /*if (child.name == "DapMinigameUI")
            {
                Button prefabButton = child.GetComponentInChildren<Button>();
                prefabButton.onClick.AddListener(() => StartCoroutine(DestroyMinigameCoroutine()));
            }*/
        }

        /*Camera[] allCameras = Camera.allCameras;
        foreach (Camera cam in allCameras)
        {
            if (cam.gameObject.name == "Main Camera")
            {
                cam.enabled = false;
                AudioListener audioListener = cam.GetComponent<AudioListener>();
                audioListener.enabled = false;
            }
        }*/
        playerCamera.SetActive(false);
        Singleton.Instance.isLure = true;
    }

    private IEnumerator DestroyMinigameCoroutine()
    {
        // playerCamera.SetActive(true); // activate main camera again
        Camera[] allCameras = Camera.allCameras;
        foreach (Camera cam in allCameras)
        {
            if (cam.gameObject.name == "Main Camera")
            {
                cam.enabled = true;
                AudioListener audioListener = cam.GetComponent<AudioListener>();
                audioListener.enabled = true;
            }
        }

        yield return new WaitForEndOfFrame();

        playerCamera.GetComponent<Camera>().enabled = true;
        AudioListener mainAudioListener = playerCamera.GetComponent<AudioListener>();
        mainAudioListener.enabled = true;

        Destroy(currentMinigame);
        // playerCam.SetActive(true);
        // Camera please = Camera.main;
        // please.gameObject.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerInteract>().enabled = true;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;

    }

    public void SpawnFishReward()
    {
        string path = "";
        switch (selectionCombType)
        {
            case CombinationType.BaitA:
                path = "Item Prefabs/Fish Object A";
                break;
            case CombinationType.BaitB:
                path = "Item Prefabs/Fish Object B";
                break;
            case CombinationType.BaitC:
                path = "Item Prefabs/Fish Object C";
                break;
            case CombinationType.BaitD:
                path = "Item Prefabs/Fish Object D";
                break;
            case CombinationType.BaitE:
                path = "Item Prefabs/Fish Object E";
                break;
            case CombinationType.BaitF:
                path = "Item Prefabs/Fish Object F";
                break;
            case CombinationType.BaitG:
                path = "Item Prefabs/Fish Object G";
                break;
            default:
                Debug.LogWarning("Invalid CombinationType in MinigameDeployUI.cs");
                return;
        }

        GameObject fishPrefab = Resources.Load<GameObject>(path);
        if (fishPrefab != null)
        {
            Instantiate(fishPrefab, fishSpawnLocation.position, fishSpawnLocation.rotation);
        }
        else
        {
            Debug.LogWarning("Fish prefab not found at path: " + path);
        }
    }

    public void OpenUI()
    {
        UIUtility.ToggleMenu(minigameDeployUI, true);
    }

    public void CloseUI()
    {
        UIUtility.ToggleMenu(minigameDeployUI, false);
    }
}
