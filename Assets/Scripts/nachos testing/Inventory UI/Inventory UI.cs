using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    /// <summary>
    /// Switches between sections
    /// </summary>

    [Header("Sections")]
    public GameObject materialsMenu;
    public GameObject littleGuysMenu;
    public GameObject fishMenu;

    private GameObject currentMenu;
    public GameObject deployUI;


    //button methods
    public void Exit()
    {
        Singleton.Instance.menuInt--;
        if (Singleton.Instance.menuInt == 0 )
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        

        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        deployUI.SetActive(true);
        this.gameObject.SetActive(false);
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
