using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnToggleUIEvent();
        popUpIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //for (int i = 0; i < popUps.Length; i++)
        //{
            if ( index == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            }
            else
            {
                popUps[popUpIndex].SetActive(false);
            }
        //}
        */
        if (popUpIndex == 0)
        {
            popUps[0].SetActive(true);
            popUps[1].SetActive(false);
        }else if (popUpIndex == 1)
        {
            popUps[0].SetActive(false);
            popUps[1].SetActive(true);
        }
    }

    public void NextPage() { popUpIndex++;}
    public void PreviousPage() { popUpIndex--;}
    public void CloseTutUI() { gameObject.SetActive(false); EventManager.OnToggleUIEvent(); }

}
