using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Any object that can be picked up by the player and put in inventory
//Data of item is held in itemData
public class Item : Interactable
{
    public ItemData itemData;

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
        PickUp();

    }

    public void PickUp()
    {
        Singleton.Instance.player.GetComponent<PlayerInventory>().AddItem(itemData);
        Destroy(this.gameObject);
    }
}
