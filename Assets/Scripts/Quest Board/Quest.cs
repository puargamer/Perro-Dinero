using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string name;
    public CombinationType combinationType;     //what to fetch
    public int reward;
    public bool complete;
    public bool claimed;        //if reward has been claimed yet

    public void Generate()
    {
        RandomName();
        RandomCombinationType();
        RandomReward();
        complete = false;
        claimed = false;
    }

    #region Generate() functions
    void RandomName()
    {
        List<string> names = new List<string>() {"name1","name2","name3", "name4"};

        name = names[UnityEngine.Random.Range(0, names.Count)];
    }

    void RandomCombinationType()
    {
        combinationType = (CombinationType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(CombinationType)).Length);
    }
    #endregion

    void RandomReward()
    {
        reward = UnityEngine.Random.Range(50, 100);
    }


    public void PrintData()
    {
        Debug.Log("Quest Name: " + name + "\n" + 
                    "CombinationType: " + combinationType + "\n" +
                    "Reward: " + reward + "\n" +
                    "Complete: " + complete
            );
    }
}
