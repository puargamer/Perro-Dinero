using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    public DayManager DayManager;

    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset preset;

    [SerializeField,Range(0,1)] private float percentOfDayPassed;

    [Header("Dev")]
    public bool DevMode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DevMode)        //dev mode: lighting is always afternoon
        {
            UpdateLighting(.5f); 
        }
        else if (Application.isPlaying)         //normal mode: dynamic lighting in play mode
        {
            percentOfDayPassed = DayManager.currentTime / (DayManager.dayLengthInMinutes * 60);
            UpdateLighting(percentOfDayPassed);
        }
        else    //lighting changes based on slider in editor
        {
            UpdateLighting(percentOfDayPassed);
        }
    }

    void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);
        directionalLight.color = preset.directionalColor.Evaluate(timePercent);
        directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 180f), 170f, 0));
    }
}
