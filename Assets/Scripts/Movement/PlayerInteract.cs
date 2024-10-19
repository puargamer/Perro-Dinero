using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public bool hitboxActive;

    public GameObject objectInHitbox;
    public PlayerInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //on click, calls target obj's interact()
        if (Input.GetMouseButtonDown(0) && objectInHitbox != null && inventory.ObjectHeld == null)
        {
            if (objectInHitbox.TryGetComponent<Interact>(out Interact _interact))
            {
                _interact.interact();
            }
        }
    }

}
