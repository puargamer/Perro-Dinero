using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craftTable : Interactable
{
    public GameObject player;
    public GameObject deployUI;
    public GameObject craftingTableUI;

    public override void Interact()
    {
        Debug.Log("craft table interact being called");
        if (!Singleton.Instance.isMenuOpened)
        {
            deployUI.SetActive(false);
            gameObject.GetComponent<craftUI>().ScrapeFromInventory();
            player.GetComponent<PlayerMovement>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //GetComponentInChildren<Canvas>().enabled = true;
            craftingTableUI.SetActive(true);
            Singleton.Instance.isMenuOpened = true;
        }
    }
}
