using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public Transform respawnPoint;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("touched death plane");
        if (other.CompareTag("Player"))
        {
            Debug.Log("detected as player");
            Debug.Log(respawnPoint.position);
            other.GetComponent<CharacterController>().enabled = false;
            //other.GetComponent<PlayerMovement>().Respawn();
            other.gameObject.transform.position = respawnPoint.position;
            other.gameObject.transform.rotation = respawnPoint.rotation;
            other.GetComponent<CharacterController>().enabled = true;
        }
        if (other.CompareTag("Golem"))
        {
            Debug.Log("detected as little guy");
            other.gameObject.transform.position = respawnPoint.position;
            other.gameObject.transform.rotation = respawnPoint.rotation;
        }
    }
}
