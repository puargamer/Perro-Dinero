using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleItem : Item
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Singleton.Instance.player.GetComponent<PlayerInventory>().InInventory(itemData) && Input.GetKeyDown(KeyCode.Mouse0)) { Debug.Log("i am speaking"); }
    }

    public override void Use()
    {
        Debug.Log("i am speaking");
    }
}
