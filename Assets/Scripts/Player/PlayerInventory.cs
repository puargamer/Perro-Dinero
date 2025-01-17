using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
    //public List<GameObject> PikminList;       //currently being held in singleton

    public int heldObjectIndex;     //1-based index referencing currently held object. 0 means no object is held

    private Dictionary<int, GameObject> hotbar = new Dictionary<int, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //InventoryArray = new ItemData[3];
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
        for(int i = 0; i < InventoryArray.Length; i++) 
        {
            if (InventoryArray[i] == null)
            {
                InventoryArray[i] = itemData;
                break;
            }
        }

        EventManager.OnInventoryEvent();
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

        EventManager.OnInventoryEvent();
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

    public bool IsFull()
    {
        for (int i = 0; i < InventoryArray.Length; i++)
        {
            if (InventoryArray[i] == null)
            {
                return false;
            }
        }

        return true;
    }
    #endregion 

    //hold item. Use when item is equipped from hotbar
    public void Equip(int i)
    {
        //if (ObjectHeld == InventoryArray[i]) { return; }
        Unequip();

        if (i < InventoryArray.Length && InventoryArray[i])
        {
            ObjectHeld = Instantiate(InventoryArray[i].item);

            heldObjectIndex = i+1;
            EventManager.OnInventoryEvent();

            isHoldingObject = true;
            EventManager.OnPlayerHoldingItemEvent();
        }

    }

    //unhold item. Used when switching to another item in hotbar
    public void Unequip()
    {
        if (ObjectHeld != null)
        {
            heldObjectIndex = 0;
            EventManager.OnInventoryEvent(); 

            ObjectHeld.GetComponent<Rigidbody>().isKinematic = false;

            //  Singleton.Instance.player.GetComponentInChildren<PlayerInteractHitbox>().RemoveFromList(ObjectHeld);
            Destroy(ObjectHeld);
            ObjectHeld = null;

            isHoldingObject = false;
            EventManager.OnPlayerHoldingItemEvent();

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
            heldObjectIndex = 0;
            isHoldingObject = false;

            EventManager.OnInventoryEvent();
            EventManager.OnPlayerHoldingItemEvent();

        }
    }

    #region Money
    public void ChangeMoney(int change)
    {
        money += change;
        EventManager.OnMoneyEvent();
        Debug.Log("eventmanager called");
    }
    #endregion
}
