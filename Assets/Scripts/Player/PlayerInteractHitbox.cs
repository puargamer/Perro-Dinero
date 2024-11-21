using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Stores a list of interactable items near the player
public class PlayerInteractHitbox : MonoBehaviour
{
    public PlayerInteract playerInteract;
    public List<GameObject> interactablesInHitbox = new List<GameObject>();

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Interactable")) { 
            //playerInteract.hitboxActive = true; 
            //playerInteract.objectInHitbox = collider.gameObject;
            interactablesInHitbox.Add(collider.gameObject);

        }
    }
    void OnTriggerExit(Collider collider) 
    { 
        if (collider.gameObject.layer == LayerMask.NameToLayer("Interactable")) { 
            //playerInteract.hitboxActive = false;
            //playerInteract.objectInHitbox = null;
            interactablesInHitbox.Remove(collider.gameObject);
        }
    }

    public void RemoveFromList(GameObject gameObject)
    {
        interactablesInHitbox.Remove(gameObject);
    }

}
