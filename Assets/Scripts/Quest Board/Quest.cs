using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string name;
    public CombinationType combinationType;     //what to fetch
    public int count;                           //how much to fetch
    public int reward;
    public bool complete;

    public void Generate()
    {

    }
}
