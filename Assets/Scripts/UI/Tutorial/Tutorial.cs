using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;

    // Start is called before the first frame update
    void Start()
    {
        popUpIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            }
            else
            {
                popUps[popUpIndex].SetActive(false);
            }
        }
    }

    public void NextPage() { popUpIndex++; }
    public void PreviousPage() { popUpIndex--; }
    public void CloseTutUI() { gameObject.SetActive(false); }

}
