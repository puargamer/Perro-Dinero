using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class OrderSign : MonoBehaviour
{
    public GameObject order;        //gameobject holding an order Script

    public TMP_Text text;
    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        CreateOrder();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();

        //refresh order when done
        if (order.GetComponent<Order>().isComplete)
        {
            RefreshOrder();
        }
    }

    void UpdateText()
    {
        string cuh = "";

        for (int i = 0; i < order.GetComponent<Order>().requests.Count; i++)
        {
            cuh += ConvertEnum(order.GetComponent<Order>().requests[i]) + "\n";
        }

        text.text = cuh;
    }

    public string ConvertEnum(CombinationType combinationType)      //converts CombinationType enum to a color as a string
    {
        switch(combinationType)
        {
            case CombinationType.BaitA: { return "Brown";}
            case CombinationType.BaitB: { return "Red"; }
            case CombinationType.BaitC: { return "Orange"; }
            case CombinationType.BaitD: { return "Yellow"; }
            case CombinationType.BaitE: { return "Blue"; }
            case CombinationType.BaitF: { return "Green"; }
            case CombinationType.BaitG: { return "Purple"; }
            default: return null;
        }
    }

    void CreateOrder()
    {
        order = new GameObject("mr cuh");
        order.transform.parent = transform;
        order.AddComponent<Order>();    //generates an order Script
        order.GetComponent<Order>().InitOrder(difficulty);
    }

    void RefreshOrder()
    {
        Destroy(order);
        CreateOrder();
    }
}
