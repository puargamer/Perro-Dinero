using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dapPregameUI : MonoBehaviour
{
    public GameObject littleguy;
    // Start is called before the first frame update
    void Start()
    {
        littleguy = GameObject.Find("dapMinigameStart");
        foreach (Transform child in littleguy.transform)
        {
            if (child.name == "dapUpLittleGuy")
            {
                littleguy = child.gameObject; break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            littleguy.GetComponent<minigameMovement>().enabled = true;
            gameObject.SetActive(false);
        }
    }
}
