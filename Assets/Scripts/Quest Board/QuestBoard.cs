using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages a list of Quests and displays them with a list of QuestVisuals.

public class QuestBoard : Interactable
{
    public Quest[] questArray = new Quest[4];

    [Header("Visuals")]
    public GameObject questBoardUICanvas;
    public QuestVisual[] questVisualArray = new QuestVisual[4];

    [Header("UI to disable")]
    public GameObject GeneralUICanvas;
    public GameObject HotbarUICanvas;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < questArray.Length; i++)
        {
            questArray[i].Generate();
            questVisualArray[i].UpdateVisual(questArray[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        OpenQuestBoardUI();
    }


    //display methods
    public void OpenQuestBoardUI()
    {
        //update quest visuals
        for (int i = 0; i < questArray.Length; i++)
        {
            questVisualArray[i].UpdateVisual(questArray[i]);
        }


        questBoardUICanvas.SetActive(true);
        GeneralUICanvas.SetActive(false);
        HotbarUICanvas.SetActive(false);
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void CloseQuestBoardUI()
    {
        questBoardUICanvas.SetActive(false);
        GeneralUICanvas.SetActive(true);
        HotbarUICanvas.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}
