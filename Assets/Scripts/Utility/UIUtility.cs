using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            GameObject.Find("Player").GetComponent<PlayerInteract>().enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Singleton.Instance.isMenuOpened = false;
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
            GameObject.Find("Player").GetComponent<PlayerInteract>().enabled = true;
        }

        menuToToggle.SetActive(isOpening);
    }

    public static void PreserveAspectRatio(RawImage rawImage)
    {
        if (rawImage.texture == null) return;

        RectTransform rt = rawImage.GetComponent<RectTransform>();
        float textureWidth = rawImage.texture.width;
        float textureHeight = rawImage.texture.height;
        float aspectRatio = textureWidth / textureHeight;

        if (rt.rect.width / rt.rect.height > aspectRatio)
        {
            rt.sizeDelta = new Vector2(rt.rect.height * aspectRatio, rt.rect.height);
        }
        else
        {
            rt.sizeDelta = new Vector2(rt.rect.width, rt.rect.width / aspectRatio);
        }
    }
}

// example usage:
// public void OpenMenu()
//{
//    ToggleMenu(UIGameObject, true);
//}
