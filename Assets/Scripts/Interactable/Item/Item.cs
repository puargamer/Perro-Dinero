using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Any object that can be put in inventory 
//Requires ItemData as a way to represent item in inventory.
public abstract class Item : Interactable
{
    public ItemData itemData;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Singleton.Instance.player.GetComponent<PlayerInventory>().InInventory(itemData) && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Use();
        }
    }

    public override void Interact()
    {
        PickUp();
    }

    public void PickUp()
    {//too many references, but works lmao
        if (Singleton.Instance.player.GetComponent<PlayerInventory>().IsFull() == false) {
        Singleton.Instance.player.GetComponent<PlayerInventory>().AddItem(itemData);
        Singleton.Instance.player.GetComponentInChildren<PlayerInteractHitbox>().RemoveFromList(this.gameObject);
        Destroy(this.gameObject);
        }
    }

    public virtual void Use()
    {
        
    }
}
