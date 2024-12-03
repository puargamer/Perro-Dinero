using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenu : MonoBehaviour
{
    //Holds list of submenus in the Tab Menu. Switches between them.

    [Header("Sections")]
    public GameObject inventoryMenu;
    public GameObject materialsMenu;
    public GameObject littleGuysMenu;
    public GameObject fishMenu;

    private GameObject currentMenu;
    public GameObject deployUI;


    //button methods

    public void OpenInventory()
    {
        if (currentMenu) { currentMenu.SetActive(false); }
        inventoryMenu.SetActive(true);
        currentMenu = inventoryMenu;
    }
    public void OpenMaterials()
    {
        if (currentMenu) { currentMenu.SetActive(false); }
        materialsMenu.SetActive(true);
        currentMenu = materialsMenu;
    }

    public void OpenLittleGuys()
    {
        if (currentMenu) { currentMenu.SetActive(false); }
        littleGuysMenu.SetActive(true);
        currentMenu = littleGuysMenu;
    }

    public void OpenFish()
    {
        if (currentMenu) { currentMenu.SetActive(false); }
        fishMenu.SetActive(true);
        currentMenu = fishMenu;
    }
}
