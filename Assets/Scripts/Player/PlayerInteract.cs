using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//allows player to interact with list of interactables in PlayerInteractHitbox
public class PlayerInteract : MonoBehaviour
{
    public PlayerInteractHitbox playerInteractHitbox;

    //public bool hitboxActive;

    //public GameObject objectInHitbox;
    public PlayerInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //on click, calls target obj's interact()
        if (Input.GetMouseButtonDown(0) && playerInteractHitbox.interactablesInHitbox.Count != 0  && inventory.ObjectHeld == null)
        {
            Debug.Log("thingy called");
            if (playerInteractHitbox.interactablesInHitbox[0].TryGetComponent<Interactable>(out Interactable _interact))
            {
                _interact.Interact();
            }
        }
    }

}
