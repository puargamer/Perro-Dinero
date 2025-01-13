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
        else if (Input.GetKeyDown(KeyCode.Tab) && Singleton.Instance.isMenuOpened && tabMenu.activeSelf) { CloseMenu(); }
    }

    public void OpenMenu()
    {
        //Singleton.Instance.isMenuOpened = true;
        EventManager.OnToggleUIEvent();

        tabMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        //Singleton.Instance.isMenuOpened = false;

        EventManager.OnToggleUIEvent();
        tabMenu.SetActive(false);
    }
}
