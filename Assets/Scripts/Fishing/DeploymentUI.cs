using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeploymentUI : MonoBehaviour
{
    public GameObject player;
    public GameObject childCanvas;
    private bool deployUIopen = false;
    public GameObject deployGrid;
    public GameObject lurePrefab;

    public GameObject playerCam;
    public GameObject tempGoon;
    public GameObject tempParent;
    public GameObject tabText;
    private GameObject selection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !Singleton.Instance.isLure)
        {
            Debug.Log("q worked");
            deployUIopen = !deployUIopen;
            childCanvas.SetActive(deployUIopen);
            //if (Singleton.Instance.menuInt <= 1) 
            //{
            player.GetComponent<PlayerMovement>().enabled = !deployUIopen;
            //}
            if (!deployUIopen)
            {
                Singleton.Instance.menuInt--;
            } 
            else
            {
                Singleton.Instance.menuInt++;
            }
            Cursor.lockState = deployUIopen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = deployUIopen;
            if (Singleton.Instance.menuInt == 0)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void AddToGrid(Sprite fishsticks)
    {
        GameObject newLure = Instantiate(lurePrefab, Vector3.zero, Quaternion.identity, deployGrid.transform);
        newLure.GetComponentInChildren<Image>().sprite = fishsticks;
        Debug.Log(fishsticks.name);
    }

    public void deploy()
    {
        tabText.SetActive(true);
        Singleton.Instance.isLure = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        deployUIopen = false;
        childCanvas.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = false;
        playerCam.SetActive(false);
        tempGoon.SetActive(true);
        tempParent.SetActive(false);
    }

    public void deploySelect()
    {
        
    }
}
