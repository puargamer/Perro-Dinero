using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//World Space representation of an item.
public class ItemWorld : Interactable
{
    public Item item;

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
        Singleton.Instance.player.GetComponent<PlayerInventory>().AddItem(item);
        Destroy(this.gameObject);
    }
}
