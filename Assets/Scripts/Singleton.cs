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
    }


    //sample globally available variable and method
    //call Singleton.instance.testInt to access 
    public int testInt;

    public void test()
    {
        Debug.Log("hello");
    }
}


