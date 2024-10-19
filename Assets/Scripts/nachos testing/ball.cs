using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : Interact
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void interact()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerInventory>().ObjectHeld = this.gameObject;
    }
}
