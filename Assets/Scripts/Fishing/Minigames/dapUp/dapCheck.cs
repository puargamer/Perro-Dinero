using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dapCheck : MonoBehaviour
{
    public GameObject playerDap;
    public GameObject fishDap;
    public GameObject dapUI;
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
                    child.gameObject.SetActive(true); break;
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
                    child.gameObject.SetActive(true); break;
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
                    child.gameObject.SetActive(true); break;
                }
            }
        }
    }
}
