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

    public float sens = 400;
    public float AngleMin;
    public float AngleMax;
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
        move = Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right;
        golemTr.position += move * speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(new Vector3(0f, jumpHeight, 0), ForceMode.Impulse);
        }
        this.transform.forward = LureCamPos.transform.forward;
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
