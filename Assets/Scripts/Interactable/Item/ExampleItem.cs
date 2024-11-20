using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleItem : ItemWorld
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Singleton.Instance.player.GetComponent<PlayerInventory>().InInventory(item) && Input.GetKeyDown(KeyCode.Mouse0)) { Debug.Log("i am speaking"); }
    }
}
