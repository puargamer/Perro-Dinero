using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Item
{
    public Light spotLight;
    public Light pointLight;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Use()
    {
        toggleLights();
    }

    void toggleLights()
    {
        spotLight.enabled = !spotLight.enabled;
        pointLight.enabled = !pointLight.enabled;
    }
}
