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
    public int fishCount;
    public int redMat;
    public int yellowMat;
    public int blueMat;
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
}


