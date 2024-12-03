using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject hotbar1;
    public GameObject hotbar2;
    public GameObject hotbar3;

    public GameObject highlight1;
    public GameObject highlight2;
    public GameObject highlight3;

    private Dictionary<int, GameObject> hotbar = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> hotbarHighlight = new Dictionary<int, GameObject>();

    [Header("Player Inventory")]
    public ItemData[] InventoryArray;
    public int heldObjectIndex;

    private void OnEnable()
    {
        PlayerInventory.InventoryEvent += UpdateHotbar;
    }

    private void OnDisable()
    {
        PlayerInventory.InventoryEvent -= UpdateHotbar;
    }

    // Start is called before the first frame update
    void Start()
    {

        hotbar.Add(0, hotbar1);
        hotbar.Add(1, hotbar2);
        hotbar.Add(2, hotbar3);

        hotbarHighlight.Add(0, highlight1);
        hotbarHighlight.Add(1, highlight2);
        hotbarHighlight.Add(2, highlight3);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHotbar()
    {
        //references PlayerInventory
        InventoryArray = Singleton.Instance.player.GetComponent<PlayerInventory>().InventoryArray;
        heldObjectIndex = Singleton.Instance.player.GetComponent<PlayerInventory>().heldObjectIndex;

        //clear hotbar
        for (int i = 0; i < hotbar.Count; i++)
        {
            //hotbar[i].GetComponent<Image>().color = Color.white;
            hotbarHighlight[i].SetActive(false);
            hotbar[i].SetActive(false);
        }

        //rewrite hotbar
        for (int i = 0; i < hotbar.Count; i++)
        {
            if (InventoryArray[i] != null)
            {
                hotbar[i].SetActive(true);
                hotbar[i].GetComponent<Image>().sprite = InventoryArray[i].icon;

            }

            //mark held item in hotbar
            if (heldObjectIndex != 0)
            {
                //hotbar[heldObjectIndex - 1].GetComponent<Image>().color = Color.green;
                hotbarHighlight[heldObjectIndex - 1].SetActive(true) ;
            }
        }
    }
}
