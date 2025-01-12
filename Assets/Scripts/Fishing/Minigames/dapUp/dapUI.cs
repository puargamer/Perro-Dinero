using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dapUpUI : MonoBehaviour
{
    public GameObject dapGame;
    public GameObject dapPregame;
    // Start is called before the first frame update
    void Start()
    {
        dapGame = GameObject.Find("dapGameParent");
        dapPregame = GameObject.Find("dapMinigameStart");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenStartUI()
    {
        dapPregame.SetActive(false);
        foreach (Transform child in transform)
        {
            if (child.name == "StartScreen")
            {
                child.gameObject.SetActive(true); break;
            }
        }
        foreach (Transform child in dapGame.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
