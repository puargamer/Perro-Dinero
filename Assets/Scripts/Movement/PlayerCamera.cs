using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject CameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //aligns cam to player
    void LateUpdate()
    {
        this.transform.position = CameraPosition.transform.position;
        transform.forward = CameraPosition.transform.forward;
    }
}
