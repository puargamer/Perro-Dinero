using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractHitbox : MonoBehaviour
{
    public PlayerInteract playerInteract;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Interactable")) { 
            playerInteract.hitboxActive = true; 
            playerInteract.objectInHitbox = collider.gameObject;


        }
    }
    void OnTriggerExit(Collider collider) 
    { 
        if (collider.gameObject.layer == LayerMask.NameToLayer("Interactable")) { 
            playerInteract.hitboxActive = false;
            playerInteract.objectInHitbox = null;
        }
    }



}
