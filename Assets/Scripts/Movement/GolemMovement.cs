using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Transform golemTr;
    public float speed;
    private Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        golemTr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right;
        golemTr.position += move * speed * Time.deltaTime;
    }
}
