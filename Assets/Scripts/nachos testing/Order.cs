using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public List<CombinationType> requests = new List<CombinationType>();
    public bool isComplete;

    public void InitOrder(int difficulty)
    {
        RandomizeOrder(difficulty);
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

        Array allColors = Enum.GetValues(typeof(CombinationType));
        for (int i = 0; i < length; i++)
        {
            requests.Add((CombinationType)UnityEngine.Random.Range(0, allColors.Length));
        }
    }

    void FinishRequest(CombinationType submit)
    {
        requests.Remove(submit);
    }



}
