using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Transform heldObjectPosition;
    public GameObject ObjectHeld;
    public bool isHoldingObject;

    public List<ItemData> InventoryList = new List<ItemData>();
    //public List<GameObject> PikminList;       //currently being held in singleton

    [Header("Hotbar")]
    public GameObject hotbar1;
    public GameObject hotbar2;
    public GameObject hotbar3;

    private int heldObjectIndex;

    private Dictionary<int, GameObject> hotbar = new Dictionary<int, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        hotbar.Add(0, hotbar1);
        hotbar.Add(1, hotbar2);
        hotbar.Add(2, hotbar3);
    }

    // Update is called once per frame
    void Update()
    {
        //hotbar
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(0);
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
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

        //"hold" object
        if (ObjectHeld != null)
        {
            ObjectHeld.transform.position = heldObjectPosition.position;
        }
        
    }

    #region Inventory Search
    public void AddItem(ItemData itemData)
    {
        InventoryList.Add(itemData);
        UpdateHotbar();
    }

    public void RemoveItem(ItemData itemData)
    {
        InventoryList.Remove(itemData);
        UpdateHotbar();
    }

    public bool InInventory(ItemData itemData)
    {
        if (InventoryList.Contains(itemData)) return true;
        else return false;
    }
    #endregion 

    public void Equip(int i)
    {
        Unequip();

        if (i < InventoryList.Count && InventoryList[i])
        {
            Debug.Log("equipping item");
            ObjectHeld = Instantiate(InventoryList[i].item);

            heldObjectIndex = i;
            hotbar[i].GetComponent<Image>().color = Color.green;
        }

    }

    public void Unequip()
    {
        if (ObjectHeld != null)
        {
            hotbar[heldObjectIndex].GetComponent<Image>().color = Color.white;

            Debug.Log("unequipping item");
            Destroy(ObjectHeld);

        }
    }

    public void Drop()
    {
        if (ObjectHeld != null)
        {
            RemoveItem(ObjectHeld.GetComponent<Item>().itemData);
            ObjectHeld = null;
        }
    }

    private void UpdateHotbar()
    {
        Debug.Log("count is " + InventoryList.Count);

        //clear hotbar
        for (int i = 0; i < hotbar.Count; i++)
        {
            hotbar[heldObjectIndex].GetComponent<Image>().color = Color.white;
            hotbar[i].SetActive(false);
        }


            //update hotbar
            for (int i = 0;  i < InventoryList.Count; i++)
        {
            if (InventoryList[i] != null)
            {
                hotbar[i].SetActive(true);
                hotbar[i].GetComponent<Image>().sprite = InventoryList[i].icon;
            }
            else
            {
                hotbar[i].SetActive(false);
            }
        }

    }
}
