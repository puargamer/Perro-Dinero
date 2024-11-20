using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Transform HoldSpace;
    public GameObject ObjectHeld;

    public List<Item> InventoryList = new List<Item>();
    //public List<GameObject> PikminList;       //currently being held in singleton

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip();
        }

        if (ObjectHeld != null)
        {
            ObjectHeld.transform.position = HoldSpace.position;
        }
        
    }

    #region Inventory Search

    public void AddItem(Item item)
    {
        InventoryList.Add(item);
    }

    public void RemoveItem(Item item)
    {
        InventoryList.Remove(item);
    }

    public bool InInventory(Item item)
    {
        if (InventoryList.Contains(item)) return true;
        else return false;
    }
    #endregion 

    public void Equip()
    {
        if (InventoryList[0])
        {
            Debug.Log("equipping item");
            ObjectHeld = Instantiate(Singleton.Instance.ExampleItem);
        }
    }
}
