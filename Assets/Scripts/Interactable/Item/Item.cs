using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//holds data of an item. Can be edited as a ScriptableObject
[CreateAssetMenu(fileName = "New Item", menuName = "Interactable/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public string description;
}
