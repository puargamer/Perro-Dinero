using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//internal clock with 3 states of time
public class DayManager : MonoBehaviour
{
    [Header("Current Time")]
    [SerializeField] private float currentTime;
    [SerializeField] private bool dayFinished;
    public enum timeOfDay { Morning, Noon, Night }
    public timeOfDay currentTimeOfDay;

    public enum weekday { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday}
    public weekday currentWeekday;

    [Header("Day Length")]
    public float dayLengthInMinutes;    //used for inspector editing
    private float dayLengthInSeconds;   //converted inspector value

    [Header("Times of Day")]
    public float morningStartInMinutes;
    private float morningStartInSeconds;
    public float noonStartInMinutes;
    private float noonStartInSeconds;
    public float nightStartInMinutes;
    private float nightStartInSeconds;

    [Header("UI")]
    public TMP_Text clockUI;
    public TMP_Text timeOfDayUI;
    public TMP_Text weekdayUI;


    // Start is called before the first frame update
    void Start()
    {
        dayLengthInSeconds = dayLengthInMinutes * 60;
        morningStartInSeconds = morningStartInMinutes * 60;
        noonStartInSeconds = noonStartInMinutes * 60;
        nightStartInSeconds = nightStartInMinutes * 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dayFinished) //progress time
        { 
            currentTime += Time.deltaTime; 
            if (currentTime >= dayLengthInSeconds) { dayFinished = true; }
        }

        UpdateTimeOfDay();
        UpdateUI();
        NextDay();
    }

    void UpdateTimeOfDay()
    {
        if (currentTime >= nightStartInSeconds) { currentTimeOfDay = timeOfDay.Night; }
        else if (currentTime >= noonStartInSeconds) { currentTimeOfDay = timeOfDay.Noon; }
        else if (currentTime >= morningStartInSeconds) { currentTimeOfDay = timeOfDay.Morning; }
    }

    void NextDay()
    {
        if (dayFinished)
        {
            dayFinished = false;
            currentTime = 0;

            if (currentWeekday == weekday.Saturday) { currentWeekday = weekday.Sunday; }
            else {currentWeekday++; }
        }
    }

    void UpdateUI()
    {
        clockUI.text = Mathf.Floor(currentTime).ToString();
        timeOfDayUI.text = currentTimeOfDay.ToString();
        weekdayUI.text = currentWeekday.ToString();
    }
}
