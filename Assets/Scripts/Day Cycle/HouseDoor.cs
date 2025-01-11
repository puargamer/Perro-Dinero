using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//interactable to reset and progress to the next day. Located in front of the house door.
public class HouseDoor : Interactable
{
    public GameObject houseDoorUI;

    public GameObject GeneralUICanvas;
    public GameObject HotbarUICanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        OpenHouseDoorUI();
    }

    public void Reset()
    {
        SaveData.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //pop up message
    public void OpenHouseDoorUI()
    {
        houseDoorUI.SetActive(true);

        GeneralUICanvas.SetActive(false);
        HotbarUICanvas.SetActive(false);
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseHouseDoorUI()
    {
        houseDoorUI.SetActive(false);

        GeneralUICanvas.SetActive(true);
        HotbarUICanvas.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}