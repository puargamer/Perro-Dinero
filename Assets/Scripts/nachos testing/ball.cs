using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : Interactable
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
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerInventory>().ObjectHeld = this.gameObject;
    }
}
