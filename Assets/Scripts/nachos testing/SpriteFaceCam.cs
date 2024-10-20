using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFaceCam : MonoBehaviour
{
    //makes sprite face camera
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.forward = player.transform.forward;
    }
}
