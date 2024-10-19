using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//spins and moves back/forth a gameobject
public class Icon : MonoBehaviour
{
    [Header("x axis")]
    public float speed;

    [Header("y axis")]
    public float yDiff;
    public float ySpeed;

    private Vector3 yMin;
    private Vector3 yMax;
    private float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        yMin = new Vector3(0, transform.position.y - yDiff, 0);
        yMax = new Vector3(0, transform.position.y + yDiff, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float percentage = Mathf.PingPong(Time.time *ySpeed, 1);

        transform.Rotate(Vector3.up * speed * Time.deltaTime);
        transform.position = Vector3.Lerp(yMin, yMax, percentage);

    }
}
