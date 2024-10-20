using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craftTable : Interact
{
    public GameObject player;
    public GameObject deployUI;

    public override void interact()
    {
        deployUI.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GetComponentInChildren<Canvas>().enabled = true;
    }
}
