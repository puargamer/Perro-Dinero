using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lureCam : MonoBehaviour
{
    public GameObject lureCamPos;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = lureCamPos.transform.position;
        transform.forward = lureCamPos.transform.forward;
    }
}
