using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public string[] welcomeMessage;
    public string[] tutorialMessage;

    public GameObject canvas;

    public bool inMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        if (!inMenu)
        {
            EventManager.OnToggleUIEvent();
            inMenu = true;
            canvas.SetActive(true);
            EventManager.OnDialogueEvent(welcomeMessage);
        }

    }

    public void tutorial()
    {
        EventManager.OnDialogueEvent(tutorialMessage);
    }

    public void exit()
    {
        EventManager.OnToggleUIEvent();
        inMenu = false; canvas.SetActive(false);
    }
}
