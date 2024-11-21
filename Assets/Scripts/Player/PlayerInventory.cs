using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Transform heldObjectPosition;
    public GameObject ObjectHeld;
    public bool isHoldingObject;

    [Header("Money")]
    public int money;

    [Header("Inventory")]
    public ItemData[] InventoryArray = new ItemData[3];
    public List<SuperItem> SuperItems = new List<SuperItem>();
    //public List<GameObject> PikminList;       //currently being held in singleton

    [Header("UI")]
    public GameObject hotbar1;
    public GameObject hotbar2;
    public GameObject hotbar3;

    public TMP_Text moneyUI;

    private int heldObjectIndex;

    private Dictionary<int, GameObject> hotbar = new Dictionary<int, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        hotbar.Add(0, hotbar1);
        hotbar.Add(1, hotbar2);
        hotbar.Add(2, hotbar3);

        SuperItems.Add(new SuperItem("name", "description"));

        InventoryArray = new ItemData[3];

    }

    // Update is called once per frame
    void Update()
    {
        //hotbar
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Equip(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Equip(2);
        }

        //drop object
        if (Input.GetKeyDown(KeyCode.R) && ObjectHeld)
        {
            Drop();
        }

        
        
    }

    private void LateUpdate()
    {
        //"hold" object
        //must be in lateupdate to remove stuttering
        if (ObjectHeld != null)
        {
            ObjectHeld.transform.position = heldObjectPosition.position;
            ObjectHeld.transform.rotation = heldObjectPosition.rotation;
            ObjectHeld.GetComponent<Rigidbody>().isKinematic = true;
        }
    }


    #region Inventory Methods
    public void AddItem(ItemData itemData)
    {
        Debug.Log("additem called");
        for(int i = 0; i < InventoryArray.Length; i++) 
        {
            Debug.Log("i is " + i);
            if (InventoryArray[i] == null)
            {
                InventoryArray[i] = itemData;
                break;
            }
        }

        UpdateHotbar();
    }

    public void RemoveItem(ItemData itemData)
    {
        for (int i = 0; i < InventoryArray.Length; i++)
        {
            if (InventoryArray[i] == itemData)
            {
                InventoryArray[i] = null;
                break;
            }
        }

        UpdateHotbar();
    }

    public bool InInventory(ItemData itemData)
    {
        for (int i = 0; i < InventoryArray.Length; i++)
        {
            if (InventoryArray[i] == itemData)
            {
                return true;
            }
        }

        return false;
    }
    #endregion 

    //hold item
    public void Equip(int i)
    {
        Unequip();

        if (i < InventoryArray.Length && InventoryArray[i])
        {
            Debug.Log("equipping item");
            ObjectHeld = Instantiate(InventoryArray[i].item);

            heldObjectIndex = i;
            hotbar[i].GetComponent<Image>().color = Color.green;
        }

    }

    //unhold item
    public void Unequip()
    {
        if (ObjectHeld != null)
        {
            hotbar[heldObjectIndex].GetComponent<Image>().color = Color.white;

            Debug.Log("unequipping item");
            ObjectHeld.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(ObjectHeld);

        }
    }

    //drop item back to world space, remove from inventory
    public void Drop()
    {
        if (ObjectHeld != null)
        {
            RemoveItem(ObjectHeld.GetComponent<Item>().itemData);
            ObjectHeld.GetComponent<Rigidbody>().isKinematic = false;
            ObjectHeld = null;
        }
    }

    private void UpdateHotbar()
    {
        Debug.Log("Length is " + InventoryArray.Length);

        //clear hotbar
        for (int i = 0; i < hotbar.Count; i++)
        {
            hotbar[heldObjectIndex].GetComponent<Image>().color = Color.white;
            hotbar[i].SetActive(false);
        }


        //update hotbar
        for (int i = 0;  i < InventoryArray.Length; i++)
        {
            if (InventoryArray[i] != null)
            {
                hotbar[i].SetActive(true);
                hotbar[i].GetComponent<Image>().sprite = InventoryArray[i].icon;
            }
            else
            {
                hotbar[i].SetActive(false);
            }
        }

    }

    #region Money
    public void ChangeMoney(int change)
    {
        money += change;
        moneyUI.text = "Money: " + money;
    }
    #endregion
}
