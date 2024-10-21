using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempStartFishing : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCam;
    public GameObject goon;
    private bool goonState = false;
    public GameObject otherMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            playerCam.SetActive(true);
            goon.SetActive(false);
            otherMenu.SetActive(true);
        }
    }
}
