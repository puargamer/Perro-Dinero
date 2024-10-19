using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public List<MaterialType> requests = new List<MaterialType>();
    public bool isComplete;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeOrder(3);
        isComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (requests.Count <= 0) { isComplete = true; }
    }

    void RandomizeOrder(int length)
    {
        requests.Clear();

        Array allColors = Enum.GetValues(typeof(MaterialType));
        for (int i = 0; i < length; i++)
        {
            requests.Add((MaterialType)UnityEngine.Random.Range(0, allColors.Length));
        }
    }

    void FinishRequest(MaterialType submit)
    {
        requests.Remove(submit);
    }



}
