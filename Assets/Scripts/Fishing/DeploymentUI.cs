using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

public class DeploymentUI : MonoBehaviour
{
    public GameObject player;
    public GameObject childCanvas;
    private bool deployUIopen = false;
    public GameObject deployGrid;
    public GameObject lurePrefab;

    public GameObject playerCam;
    //public GameObject fishingCamParent;
    public Camera fishingCam;
    //public GameObject tempGoon;
    public GameObject tempParent;
    public GameObject tabText;
    public GameObject deploySelection;
    public tempStartFishing startFishingManager;

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
            deploySelection = null;
            deployUIopen = !deployUIopen;
            childCanvas.SetActive(deployUIopen);
            //if (Singleton.Instance.menuInt <= 1) 
            //{
            player.GetComponent<PlayerMovement>().enabled = !deployUIopen;
            //}
            if (!deployUIopen)
            {
                Singleton.Instance.isMenuOpened = false;
            } 
            else
            {
                Singleton.Instance.isMenuOpened = true;
            }
            Cursor.lockState = deployUIopen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = deployUIopen;
            if (!Singleton.Instance.isMenuOpened)
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

    public void AddToGrid(Sprite fishsticks, GameObject littleGuy)
    {
        GameObject newLure = Instantiate(lurePrefab, Vector3.zero, Quaternion.identity, deployGrid.transform);
        Image lureImage = newLure.GetComponentInChildren<Image>();

        lureImage.sprite = fishsticks;
        lureImage.preserveAspect = true; // remote stretch

        //newLure.GetComponentInChildren<Image>().sprite = fishsticks;
        newLure.GetComponent<ButtonReferenceHolder>().littleGuy = littleGuy;
        //Debug.Log(fishsticks.name);
    }

    public void deploy()
    {
        if (deploySelection != null)
        {
            tabText.SetActive(true);
            Singleton.Instance.isLure = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            deployUIopen = false;
            childCanvas.SetActive(false);
            player.GetComponent<PlayerMovement>().enabled = false;
            playerCam.SetActive(false);
            fishingCam = deploySelection.GetComponentInChildren<Camera>();
            fishingCam.enabled = true;
            deploySelection.GetComponentInChildren<lureCam>().enabled = true;
            //fishingCamParent.SetActive(true);
            //fishingCamParent.transform.parent = deploySelection.transform;
            //fishingCamParent.transform.localPosition = new Vector3(0f, 5f, -15f);
            //fishingCamParent.transform.localRotation = Quaternion.identity;
            deploySelection.GetComponent<LittleGuyNav>().isBeingControlled = true;
            deploySelection.GetComponent<GolemMovement>().enabled = true;
            deploySelection.GetComponent<NavMeshAgent>().enabled = false;
            //deploySelection.transform.rotation = Quaternion.identity;
            deploySelection.GetComponentInChildren<SpriteRenderer>().gameObject.transform.localRotation = Quaternion.identity;
            deploySelection.GetComponentInChildren<SpriteFaceCam>().enabled = false;
            //deploySelection.GetComponent<GolemMovement>().LureCamPos = fishingCam.gameObject;
            //deploySelection.transform.position = new Vector3(deploySelection.transform.position.x, .1f, deploySelection.transform.position.z);
            //tempGoon.SetActive(true);
            startFishingManager.UpdateGoon(deploySelection);
            tempParent.SetActive(false);
        }
    }
}
