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
            Debug.Log("mat count: " + mats.Count);
        }
    }


    //sample globally available variable and method
    //call Singleton.instance.testInt to access 
    public int testInt;
    public GameObject player;
    public int fishCount;
    public List<int> mats;
    public List<GameObject> BaitALittleGuys;
    public List<GameObject> BaitBLittleGuys;
    public List<GameObject> BaitCLittleGuys;
    public List<GameObject> BaitDLittleGuys;
    public List<GameObject> BaitELittleGuys;
    public List<GameObject> BaitFLittleGuys;
    public List<GameObject> BaitGLittleGuys;

    public List<GameObject> equippedLittleGuys;
    public List<GameObject> stashedLittleGuys;

    public bool isMenuOpened = false;
    public bool isLure = false;

    private int nextId = 0; // keeps track of the next available id

    #region Prefab List
    public GameObject ExampleItem;
    #endregion

    public void test()
    {
        Debug.Log("hello");
    }

    public void CollectMat(int index)
    {
        mats[index]++;
    }

    public void RemoveMat(int index)
    {
        mats[index]--;
    }

    // we are changing these post demo this is so garbage rn
    public void EquipGuy(GameObject guy)
    {
        if (stashedLittleGuys.Contains(guy)) {
            stashedLittleGuys.Remove(guy);
        }
        equippedLittleGuys.Add(guy);

    }

    public void StashGuy(GameObject guy)
    {
        equippedLittleGuys.Remove(guy);
        stashedLittleGuys.Add(guy);
    }

    public int GrabNewId()
    {
        return nextId++;
    }

    // saving and loading
    public List<LittleGuyData> GetAllLittleGuysData()
    {
        List<LittleGuyData> allLittleGuysData = new List<LittleGuyData>();

        foreach (var guy in equippedLittleGuys)
        {
            allLittleGuysData.Add(guy.GetComponent<LittleGuyNav>().GetLittleGuyData());
        }

        foreach (var guy in stashedLittleGuys)
        {
            allLittleGuysData.Add(guy.GetComponent<LittleGuyNav>().GetLittleGuyData());
        }

        return allLittleGuysData;
    }

    public void SetAllLittleGuysData(List<LittleGuyData> allLittleGuysData)
    {
        equippedLittleGuys.Clear(); // nuke the current little guys
        stashedLittleGuys.Clear();
        foreach (var data in allLittleGuysData) // respawn them all, might have to ask factory to do this?
        {
            GameObject newGuy = Instantiate(littleGuyPrefab); // need to set location
            newGuy.GetComponent<LittleGuyNav>().SetLittleGuyData(data);

            if (data.isInPen)
            {
                stashedLittleGuys.Add(newGuy);
                newGuy.SetActive(false);
            }
            else
            {
                equippedLittleGuys.Add(newGuy);
            }
        }
    }
    public void SetNextId(int next)
    {
        nextId = next;
    }
}


