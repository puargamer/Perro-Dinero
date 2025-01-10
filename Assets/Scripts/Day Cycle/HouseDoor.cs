using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//interactable to reset and progress to the next day. Located in front of the house door.
public class HouseDoor : Interactable
{
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
        Reset();
    }

    void Reset()
    {
        SaveData.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //pop up message
    void OpenMessage()
    {

    }

    void CloseMessage()
    {

    }
}
