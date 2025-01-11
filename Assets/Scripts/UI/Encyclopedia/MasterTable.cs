using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// New plan to dynamically add things to encyclopedia and/or everywhere else
/// not implemented until post deadline
/// 
/// </summary>

//[System.Serializable]
//public class EncyclopediaItem
//{
//    public string name;
//    public string description;
//    public Sprite icon;

//    public virtual void DisplayInfo(TMP_Text nameText, TMP_Text descText, Image iconImage)
//    {
//        nameText.text = name;
//        descText.text = description;
//        iconImage.sprite = icon;
//        iconImage.gameObject.SetActive(true);
//    }
//}

//[System.Serializable]
//public class Lure : EncyclopediaItem
//{
//    public int[] catchableFishIndices; 

//    public override void DisplayInfo(TMP_Text nameText, TMP_Text descText, Image iconImage)
//    {
//        base.DisplayInfo(nameText, descText, iconImage);
//        // extra functionality
//    }
//}

//[System.Serializable]
//public class CatchableFish : EncyclopediaItem
//{
//    public float catchChance;

//    public override void DisplayInfo(TMP_Text nameText, TMP_Text descText, Image iconImage)
//    {
//        base.DisplayInfo(nameText, descText, iconImage);
//        // maybe show catch rates?
//    }
//}

//public class MasterTable
//{
//    public EncyclopediaItem[] items;  // dynamically store fish and lures

//    public MasterTable()
//    {
//        items = new EncyclopediaItem[0];
//    }

//    // dynamically add items, maybe change implementation to dictionary like recipebook
//    public void AddItem(EncyclopediaItem item)
//    {
//        Array.Resize(ref items, items.Length + 1);
//        items[items.Length - 1] = item;
//    }
//}
