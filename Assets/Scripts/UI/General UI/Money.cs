using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Updates Money UI counter
public class Money : MonoBehaviour
{
    public TMP_Text moneyUI;

    private void Update()
    {
        if (GameObject.Find("Player") != null)
        {
            moneyUI.text = "$" + GameObject.Find("Player").GetComponent<PlayerInventory>().money.ToString();
        }
    }
}
