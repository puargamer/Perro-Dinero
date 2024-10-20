using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    /// <summary>
    /// opens Inventory UI
    /// </summary>

    public GameObject inventoryUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1)) { OpenMenu(); }
    }

    void OpenMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;

        inventoryUI.SetActive(true);
    }
}
