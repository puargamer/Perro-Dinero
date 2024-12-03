using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar_Inventory_UI : MonoBehaviour
{
    public Image hotbar1;
    public Image hotbar2;
    public Image hotbar3;

    public TMP_Text objectName;
    public TMP_Text objectDescription;

    private PlayerInventory playerInventory;

    private Dictionary<int, Image> hotbar = new Dictionary<int, Image>();

    // Start is called before the first frame update
    void Awake()
    {
        playerInventory = Singleton.Instance.player.GetComponent<PlayerInventory>();

        hotbar.Add(0, hotbar1);
        hotbar.Add(1, hotbar2);
        hotbar.Add(2, hotbar3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        //reset text
        objectName.text = "";
        objectDescription.text = "";

        //update images
        for (int i = 0; i < playerInventory.InventoryArray.Length; i++)
        {
            if (playerInventory.InventoryArray[i] != null)
            {
                //hotbar[i].SetActive(true);
                hotbar[i].sprite = playerInventory.InventoryArray[i].icon;
            }
            else
            {
                hotbar[i].sprite = null;
            }
        }
    }

    public void DisplayItemInfo(int i)
    {
        if (playerInventory.InventoryArray[i] != null)
        {
            objectName.text = playerInventory.InventoryArray[i].name;
            objectDescription.text = playerInventory.InventoryArray[i].description;
        }
    }
}
