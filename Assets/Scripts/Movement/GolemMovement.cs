using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Transform golemTr;
    private Collider fishCol;
    public float speed = 7;
    public float jumpHeight = 10;
    private Vector3 move;
    public bool hasFish = false;

    public float sens = 400;
    public float AngleMin = -20;
    public float AngleMax = 80;
    float xRotation, yRotation;
    public GameObject LureCamPos;

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
        RotationCheck();
        doIdropFish();
        move = Input.GetAxis("Vertical") * LureCamPos.transform.forward + Input.GetAxis("Horizontal") * LureCamPos.transform.right;
        move = new Vector3(move.x, 0f, move.z);
        //move = move.normalized;   
        //move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //golemTr.position += move * speed * Time.deltaTime;
        var speedFactor = (speed - rb.velocity.magnitude) / speed; // hard coded
        //rb.AddForce(move, ForceMode.Impulse); //  * speedFactor
        rb.AddForce(move * speed, ForceMode.Force);
        //rb.AddForce(move, ForceMode.Impulse); // last working
        //moveCharacter(move);
        //transform.Translate(move * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(new Vector3(0f, jumpHeight, 0), ForceMode.Impulse);
        }
        transform.forward = LureCamPos.transform.forward;
    }

    void moveCharacter(Vector3 direction)
    {
        //rb.velocity += direction * speed * Time.deltaTime;
        rb.AddForce(direction, ForceMode.Impulse);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -transform.up, fishCol.bounds.extents.y + 0.1f);
    }

    void RotationCheck()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sens;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, AngleMin, AngleMax);
        LureCamPos.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    public void doIdropFish()
    {
        if (transform.GetChild(transform.childCount - 1).gameObject.tag != "Fish") { hasFish = false; }
    }
}
