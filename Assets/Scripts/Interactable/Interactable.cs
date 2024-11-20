using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inherited class of any interactable objects
public abstract class Interactable : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //what happens if player interacts with it
    public abstract void Interact();
}
