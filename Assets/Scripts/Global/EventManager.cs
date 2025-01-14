using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//holds all events in the game 
//events are used to tell scripts that certain things are happening in other scripts

public static class EventManager 
{
    //events
    #region Events
    //player inventory is updated
    public static event UnityAction InventoryEvent;
    public static event UnityAction MoneyEvent;

    //Player can use/interact with an item
    public static event UnityAction PlayerCanInteractEvent;
    public static event UnityAction PlayerHoldingItemEvent;

    //Toggle "Default" UI elements
    public static event UnityAction ToggleUIEvent;

    //Update Clock UI
    public static event UnityAction<float,float> ClockUIEvent;

    //Save and Load
    public static event UnityAction SaveEvent;
    public static event UnityAction LoadEvent;

    //Dialogue
    public static event UnityAction<string[]> DialogueEvent;

    #endregion

    //invoke methods
    #region Invoke Methods
    //player inventory is updated
    public static void OnInventoryEvent() => InventoryEvent?.Invoke();
    public static void OnMoneyEvent() => MoneyEvent?.Invoke();

    //Player can use/interact with an item
    public static void OnPlayerCanInteractEvent() => PlayerCanInteractEvent?.Invoke();
    public static void OnPlayerHoldingItemEvent() => PlayerHoldingItemEvent?.Invoke();

    //Toggle "Default" UI elements
    public static void OnToggleUIEvent() => ToggleUIEvent?.Invoke();

    //Update Clock UI
    public static void OnClockUIEvent(float time, float dayLength) => ClockUIEvent?.Invoke(time,dayLength);

    //Save and Load
    public static void OnSaveEvent() => SaveEvent?.Invoke();
    public static void OnLoadEvent() => LoadEvent?.Invoke();

    //Dialogue
    public static void OnDialogueEvent(string[] stringArray) => DialogueEvent?.Invoke(stringArray);
    #endregion
}
