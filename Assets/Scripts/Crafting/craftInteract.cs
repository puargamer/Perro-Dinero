using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craftTable : Interactable
{
    public GameObject player;
    public GameObject deployUI;

    public override void Interact()
    {
        if (!Singleton.Instance.isMenuOpened)
        {
            deployUI.SetActive(false);
            gameObject.GetComponent<craftUI>().ScrapeFromInventory();
            player.GetComponent<PlayerMovement>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GetComponentInChildren<Canvas>().enabled = true;
            Singleton.Instance.isMenuOpened = true;
        }
    }
}
