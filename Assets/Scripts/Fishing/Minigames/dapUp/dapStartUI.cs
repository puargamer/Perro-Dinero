using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dapUpStartUI : MonoBehaviour
{
    public GameObject playerDap;
    public GameObject fishDap;
    // Start is called before the first frame update
    void Awake()
    {
        playerDap = GameObject.Find("PlayerHand");
        fishDap = GameObject.Find("FishHand");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDap == null)
        {
            playerDap = GameObject.Find("PlayerHand");
            fishDap = GameObject.Find("FishHand");
        }
        if (Input.GetMouseButton(0))
        {
            playerDap.GetComponent<playerDap>().stillDapping = true;
            fishDap.GetComponent<fishDap>().stillDapping = true;
            gameObject.SetActive(false);
        }
    }
}
