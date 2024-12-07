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
            interactablesInHitbox.Add(collider.gameObject);

            if (interactablesInHitbox.Count == 1) { EventManager.OnPlayerCanInteractEvent(); Debug.Log("cuh1"); }
        }
    }
    void OnTriggerExit(Collider collider) 
    { 
        if (collider.gameObject.layer == LayerMask.NameToLayer("Interactable")) { 
            interactablesInHitbox.Remove(collider.gameObject);

            if (interactablesInHitbox.Count == 0) { EventManager.OnPlayerCanInteractEvent(); Debug.Log("cuh2"); }
        }
    }

    //used to remove object from list when it's deleted while in the trigger
    public void RemoveFromList(GameObject gameObject)
    {
        interactablesInHitbox.Remove(gameObject);

        if (interactablesInHitbox.Count == 0) { EventManager.OnPlayerCanInteractEvent(); Debug.Log("cuh3"); }
    }

}
