using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameUIInteract : Interactable
{
    public GameObject MinigameDeployUI;
    public override void Interact()
    {
        Debug.Log("minigame ui interact being called!");
        if (!Singleton.Instance.isMenuOpened)
        {
            UIUtility.ToggleMenu(MinigameDeployUI, true);
        }
    }
}
