using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        if (mats.Count < Enum.GetValues((typeof(MaterialType))).Length)
        {
            int goon2 = 0;
            int goon = Enum.GetValues((typeof(MaterialType))).Length - mats.Count;
            while (goon > 0)
            {
                if (goon2 >= 50)
                {
                    Debug.Log("u r gooning too much");
                    break;
                }
                goon2++;
                mats.Add(0);
                goon--;
            }
        }
    }


    //sample globally available variable and method
    //call Singleton.instance.testInt to access 
    public int testInt;
    public int fishCount;
    public List<int> mats;
    public List<GameObject> redLittleGuys;
    public List<GameObject> yellowLittleGuys;
    public List<GameObject> blueLittleGuys;
    public List<GameObject> greenLittleGuys;
    public List<GameObject> purpleLittleGuys;
    public List<GameObject> orangeLittleGuys;

    public void test()
    {
        Debug.Log("hello");
    }

    public void CollectMat(int index)
    {
        mats[index]++;
    }
}


