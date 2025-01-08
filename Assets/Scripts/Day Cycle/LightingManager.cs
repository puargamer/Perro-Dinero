using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    public DayManager DayManager;

    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset preset;

    [SerializeField] private float timeOfDay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Application.isPlaying)
        {
            /*
            timeOfDay += Time.deltaTime;
            timeOfDay %= 24;
            UpdateLighting(timeOfDay/24);
            */

            timeOfDay = DayManager.currentTime;
            UpdateLighting(timeOfDay / (DayManager.dayLengthInMinutes * 60));
        }
        else
        {
            UpdateLighting(timeOfDay);        //default lighting in edit mode is afternoon
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
