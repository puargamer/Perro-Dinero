using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDap : MonoBehaviour
{
    public float difficulty = 1f;
    private Vector3 yPos;
    public bool stillDapping = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stillDapping)
        {
            yPos = transform.position;
            yPos.y = Mathf.Clamp(transform.position.y, -.9f, -.2f);
            transform.position = yPos + new Vector3(-.15f, -.2f, 0f) * Time.deltaTime * difficulty;
            transform.position += Input.GetAxis("Vertical") * transform.up * Time.deltaTime;
        }
    }
}
