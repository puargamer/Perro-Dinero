using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sign : Interact
{
    public GameObject SignObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void interact()
    {
        SignObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
