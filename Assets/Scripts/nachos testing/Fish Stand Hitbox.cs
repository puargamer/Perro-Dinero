using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishStandHitbox : MonoBehaviour
{
    public FishStand fishStand;
    public bool HitboxActive;
    public bool DebugMode;      //allows all fish to enter instead of following the order system

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fish")
        {
            if (DebugMode)
            {
                fishStand.score++;
                fishStand.dialogue.StartDialogue();
                fishStand.audioSource.Play();
                Destroy(other.gameObject);
            }
            else
            {
                //follow quest
                if (fishStand.orderSign.order.GetComponent<Order>().requests.Contains(other.gameObject.GetComponent<NachosFish>().fishType))
                {
                    fishStand.orderSign.order.GetComponent<Order>().FinishRequest(other.gameObject.GetComponent<NachosFish>().fishType);
                    fishStand.score++;
                    fishStand.dialogue.StartDialogue();
                    fishStand.audioSource.Play();
                    Destroy(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HitboxActive = false;
    }
}
