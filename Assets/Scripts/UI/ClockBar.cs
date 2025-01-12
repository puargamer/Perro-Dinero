using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockBar : MonoBehaviour
{
    public Image clockBar;

    private void OnEnable()
    {
        EventManager.ClockUIEvent += UpdateClock;
    }
    private void OnDisable()
    {
        EventManager.ClockUIEvent -= UpdateClock;
    }

    void UpdateClock(float time, float dayLength)
    {
        clockBar.fillAmount = Mathf.Clamp(time/dayLength, 0, 1);
    }
}
