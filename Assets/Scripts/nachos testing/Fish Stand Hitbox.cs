using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishStandHitbox : MonoBehaviour
{
    public FishStand fishStand;
    public bool HitboxActive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fish")
        {
            fishStand.score++;
            fishStand.dialogue.StartDialogue();
            fishStand.audioSource.Play();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HitboxActive = false;
    }
}
