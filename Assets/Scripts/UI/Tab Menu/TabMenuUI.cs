using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenuUI : MonoBehaviour
{
    //Put in the parent of object holding TabMenu.cs.
    //Contains methods to open/close the Tab Menus. 

    public GameObject tabMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !Singleton.Instance.isMenuOpened) { OpenMenu(); }
        else if (Input.GetKeyDown(KeyCode.Tab) && Singleton.Instance.isMenuOpened) { CloseMenu(); }
    }

    public void OpenMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Singleton.Instance.isMenuOpened = true;

        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;

        tabMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Singleton.Instance.isMenuOpened = false;

        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;

        tabMenu.SetActive(false);
    }
}
