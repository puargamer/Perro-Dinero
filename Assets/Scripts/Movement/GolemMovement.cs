using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Transform golemTr;
    private Collider fishCol;
    public float speed;
    public float jumpHeight;
    private Vector3 move;
    public bool hasFish = false;

    // Start is called before the first frame update
    void Start()
    {
        golemTr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        fishCol = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right;
        golemTr.position += move * speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(new Vector3(0f, jumpHeight, 0), ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -transform.up, fishCol.bounds.extents.y + 0.1f);
    }
}
