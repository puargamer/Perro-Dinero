using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    private Vector3 move;
    private float yVelocity;

    [Header("States")]
    public bool isGrounded;

    [Header("Speed")]
    public float speed;
    public float gravityValue;

    [Header("Camera")]
    public float sens = 400;
    public float AngleMin;       //max angle in y axis the camera can rotate
    public float AngleMax;

    float xRotation, yRotation;

    public GameObject CameraPositionParent;

    [Header("Audio")]
    public AudioSource audioSource;

    public GameObject face;     //object that points where the cam is looking without y data
    public GameObject playerModel;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    //aligns cam to player
    void Update()
    {
        GroundCheck();
        MoveCheck();
        GravityCheck();
        RotationCheck();
    }

    void GroundCheck()
    {
        if (characterController.isGrounded) { isGrounded = true; }
        else { isGrounded = false; }
    }
    void MoveCheck()
    {
        move = Input.GetAxis("Vertical") * face.transform.forward + Input.GetAxis("Horizontal") * face.transform.right;
        characterController.Move(move * speed * Time.deltaTime);

        bool moving = move != Vector3.zero ? true : false;

        //make sounds when moving
        if (moving) { if (!audioSource.isPlaying) { audioSource.pitch = Random.Range(1f, 1.5f); audioSource.Play(); } }


        if (moving)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, move, 5 * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
    void GravityCheck()
    {
        if (!isGrounded) { yVelocity += gravityValue * Time.deltaTime; }
        characterController.Move(new Vector3(0, yVelocity, 0));
    }
    void RotationCheck()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sens;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, AngleMin, AngleMax);
        face.gameObject.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        CameraPositionParent.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
