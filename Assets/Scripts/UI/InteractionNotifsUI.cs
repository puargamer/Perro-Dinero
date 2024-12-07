using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNotifsUI : MonoBehaviour
{
    public GameObject interactNotif;
    public GameObject holdItemNotif;

    private void OnEnable()
    {
        EventManager.PlayerCanInteractEvent += ToggleInteractNotif;
        EventManager.PlayerHoldingItemEvent += ToggleHoldItemNotif;
    }

    private void OnDisable()
    {
        EventManager.PlayerCanInteractEvent -= ToggleInteractNotif;
        EventManager.PlayerHoldingItemEvent -= ToggleHoldItemNotif;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //tells player can interact
    void ToggleInteractNotif()
    {
        if (interactNotif.activeSelf) { interactNotif.SetActive(false); } else { interactNotif.SetActive(true);}
    }

    //tells player can use/drop item
    void ToggleHoldItemNotif()
    {
        Debug.Log("adsof");
        if (holdItemNotif.activeSelf) { holdItemNotif.SetActive(false); } else { holdItemNotif.SetActive(true); }
    }
}
