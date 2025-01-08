using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIUtility
{
    public static void ToggleMenu(GameObject menuToToggle, bool isOpening)
    {
        if (isOpening)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Singleton.Instance.isMenuOpened = true;
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Singleton.Instance.isMenuOpened = false;
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        }

        menuToToggle.SetActive(isOpening);
    }
}

// example usage:
// public void OpenMenu()
//{
//    ToggleMenu(UIGameObject, true);
//}
