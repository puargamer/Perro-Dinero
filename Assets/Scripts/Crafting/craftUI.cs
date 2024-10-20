using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class craftUI : MonoBehaviour
{
    public GameObject player;
    public RawImage[] craftIngredients;
    public List<Texture2D> items;
    public List<TMP_Text> textCounts;
    public List<int> counts;
    private int currSelected;
    public LittleGuyFactory lGF;
    [SerializeField]
    private Transform littleGuySpawnArea;
    

    void Start()
    {
        ResetCraft();
        ScrapeFromInventory();
    }

    private void ScrapeFromInventory()
    {
        counts[0] = Singleton.Instance.mats[0];
        textCounts[0].text = counts[0].ToString();
        counts[1] = Singleton.Instance.mats[1];
        textCounts[1].text = counts[1].ToString();
        counts[2] = Singleton.Instance.mats[2];
        textCounts[2].text = counts[2].ToString();
    }

    public void CloseCraft()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GetComponentInChildren<Canvas>().enabled = false;
        ResetCraft();
    }

    public void ResetCraft()
    {
        craftIngredients[0].texture = null;
        craftIngredients[0].color = Color.white;
        craftIngredients[1].texture = null;
        craftIngredients[1].color = Color.white;
        currSelected = 0;
        ScrapeFromInventory();
    }

    public void AddRed()
    {
        if (currSelected <= 1 && counts[0] >= 1)
        {
            textCounts[0].text = (counts[0] - 1).ToString();
            counts[0]--;
            craftIngredients[currSelected].texture = items[0];
            craftIngredients[currSelected].color = Color.red;
            currSelected++;
        }
    }

    public void AddBlue()
    {
        if (currSelected <= 1 && counts[1] >= 1)
        {
            textCounts[1].text = (counts[1] - 1).ToString();
            counts[1]--;
            craftIngredients[currSelected].texture = items[1];
            craftIngredients[currSelected].color = Color.blue;
            currSelected++;
        }
    }

    public void AddYellow()
    {
        if (currSelected <= 1 && counts[2] >= 1)
        {
            textCounts[2].text = (counts[2] - 1).ToString();
            counts[2]--;
            craftIngredients[currSelected].texture = items[2];
            craftIngredients[currSelected].color = Color.yellow;
            currSelected++;
        }
    }

    public void CraftGuy()
    {
        if (currSelected == 2)
        {
            Singleton.Instance.mats[0] = counts[0];
            Singleton.Instance.mats[1] = counts[1];
            Singleton.Instance.mats[2] = counts[2];
            ResetCraft();
            MaterialType mat = (MaterialType)2;
            Singleton.Instance.redLittleGuys.Add(lGF.CreateLittleGuy(littleGuySpawnArea.position, mat));
        }
    }
}
