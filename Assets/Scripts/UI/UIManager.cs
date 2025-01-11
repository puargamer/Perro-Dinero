using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//contains public method to handle a pop-up menu
//
//ToggleUI() will:
//-hide/show canvases holding ui elements that are typically always on (ex.GeneralUI, HotbarUI)
//-disable player movement, interaction, and enable cursor

//called when you want to open a pop-up menu and hide everything behind it

public class UIManager : MonoBehaviour
{
    public List<GameObject> canvasList = new List<GameObject>(); //update this list in inspector when a new "always on" ui is created

    private void OnEnable()
    {
        EventManager.ToggleUIEvent += ToggleUI;
    }
    private void OnDisable()
    {
        EventManager.ToggleUIEvent -= ToggleUI;
    }

    public void ToggleUI()
    {
        //toggle ui
        for (int i = 0; i < canvasList.Count; i ++)
        {
            canvasList[i].SetActive(!canvasList[i].activeInHierarchy);
        }

        //toggle player options
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled  = !GameObject.Find("Player").GetComponent<PlayerMovement>().enabled;
        GameObject.Find("Player").GetComponent<PlayerInteract>().enabled = !GameObject.Find("Player").GetComponent<PlayerInteract>().enabled;
        Cursor.lockState = (Cursor.lockState == CursorLockMode.None) ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !Cursor.visible;
    }
}
