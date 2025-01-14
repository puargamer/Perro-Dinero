using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dapCheck : MonoBehaviour
{
    public GameObject playerDap;
    public GameObject fishDap;
    public GameObject dapUI;
    public GameObject playerCam;
    // Start is called before the first frame update
    private void Awake()
    {
        playerDap = GameObject.Find("PlayerHand");
        fishDap = GameObject.Find("FishHand");
        dapUI = GameObject.Find("DapMinigameUI");
        foreach (Transform child in dapUI.transform)
        {
            if (child.name == "WinScreen")
            {
                dapUI = child.gameObject; break;
            }
        }
        playerCam = GameObject.Find("Player");
        foreach (Transform child in playerCam.transform)
        {
            if (child.name == "Camera Position Parent")
            {
                playerCam = child.gameObject;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "hitbox")
        {
            playerDap.GetComponent<playerDap>().stillDapping = false;
            fishDap.GetComponent<fishDap>().stillDapping = false;
            Debug.Log("dapped up on cuh");
            dapUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            foreach( Transform child in dapUI.transform)
            {
                if (child.name == "Win")
                {
                    child.gameObject.SetActive(true); 
                    break;
                }
            }
        }
        else if (other.gameObject.name == "perfectHitbox")
        {
            playerDap.GetComponent<playerDap>().stillDapping = false;
            fishDap.GetComponent<fishDap>().stillDapping = false;
            Debug.Log("perfect dap on cuh no balls fr fr");
            dapUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            foreach (Transform child in dapUI.transform)
            {
                if (child.name == "PerfectWin")
                {
                    child.gameObject.SetActive(true); 
                    break;
                }
            }
        }
        else if (other.gameObject.name == "FailCube")
        {
            playerDap.GetComponent<playerDap>().stillDapping = false;
            fishDap.GetComponent<fishDap>().stillDapping = false;
            Debug.Log("bro does not know the dap up distance");
            dapUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            foreach (Transform child in dapUI.transform)
            {
                if (child.name == "Lose")
                {
                    child.gameObject.SetActive(true); 
                    break;
                }
            }
        }
        SetButtonListener(dapUI.transform);
    }



    private void SetButtonListener(Transform parent) // sets the button to destroy the prefab
    {
        foreach (Transform child in parent)
        {
            if (child.name == "Button") 
            {
                Button button = child.GetComponent<Button>();
                button.onClick.AddListener(DestroyMinigamePrefab);
            }
        }
    }

    private void DestroyMinigamePrefab()
    {
        GameObject minigamePrefab = GameObject.FindGameObjectWithTag("Minigame");
        playerCam.SetActive(true);
        playerCam.transform.parent.GetComponent<PlayerInteract>().enabled = true;
        playerCam.transform.parent.GetComponent<PlayerMovement>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Singleton.Instance.isLure = false;
        Destroy(minigamePrefab);
    }
}
