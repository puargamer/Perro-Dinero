using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    /// <summary>
    /// opens Inventory UI
    /// </summary>

    public GameObject inventoryUI;
    public GameObject deployUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && Singleton.Instance.menuInt == 0) { OpenMenu(); }
    }

    void OpenMenu()
    {
        deployUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Singleton.Instance.menuInt++;

        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;

        inventoryUI.SetActive(true);
    }
}
