using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NachosFish : Interact
{
    public bool isHeld;
    public bool throwDelay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isHeld && throwDelay == false) { isHeld = false; }
    }

    void LateUpdate()
    {
        if (isHeld) { transform.position = GameObject.Find("Player").GetComponent<PlayerMovement>().heldObjectPos.transform.position; }
    }

    public override void interact()
    {
        isHeld = true;
        throwDelay = true;
        StartCoroutine(NextLineWait());
    }

    IEnumerator NextLineWait()
    {
        yield return new WaitForSeconds(.3f);
        throwDelay = false;
    }


}
