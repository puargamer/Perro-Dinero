using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class OrderSign : MonoBehaviour
{
    public GameObject order;        //gameobject holding an order Script

    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        order = new GameObject("mr cuh");
        order.transform.parent = transform;
        order.AddComponent<Order>();    //generates an order Script

    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        string cuh = "";

        for (int i = 0; i < order.GetComponent<Order>().requests.Count; i++)
        {
            cuh += order.GetComponent<Order>().requests[i] + "\n";
        }

        text.text = cuh;
    }
}
