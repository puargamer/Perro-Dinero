using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField]private Vector3 move;
    [SerializeField]
    private float yVelocity;

    public float vertical;
    public float horizontal;

    [Header("States")]
    public bool isGrounded;
    public bool isSprinting;

    [Header("Speed")]
    public float speed;
    public float sprintSpeed;
    public float gravityValue;
    public float jumpHeight;

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

    public GameObject heldObjectPos;

    public bool cuh;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    //aligns cam to player
    void Update()
    {
        GroundCheck();
        SprintCheck();
        MoveCheck();
        GravityCheck();
        JumpCheck();
        RotationCheck();
    }

    void GroundCheck()
    {
        if (characterController.isGrounded) { isGrounded = true; }
        else { isGrounded = false; }
    }

    void SprintCheck()
    {
        if (Input.GetKey(KeyCode.LeftShift)) { isSprinting = true; }
        else { isSprinting = false; }
    }

    void MoveCheck()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        move = Input.GetAxis("Vertical") * face.transform.forward + Input.GetAxis("Horizontal") * face.transform.right;

        if (move.magnitude > 1) { move = move.normalized; }

        //move player
        float _speed = speed;
        if (isSprinting) { _speed = sprintSpeed; }

        characterController.Move(move * _speed * Time.deltaTime);

        bool moving = vertical != 0 || horizontal != 0;

        //make sounds when moving
        if (moving) { if (!audioSource.isPlaying) { audioSource.pitch = Random.Range(1f, 1.5f); audioSource.Play(); } }

        //rotate player
        if (moving)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, move, 10 * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    void GravityCheck()
    {
        if (!isGrounded) { yVelocity += gravityValue * Time.deltaTime; }
        else { yVelocity = -.25f; }
    }

    void JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) { yVelocity = jumpHeight; }
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
    //public void Respawn()
    //{
    //    Debug.Log("called respawn within player!");
    //    yVelocity = 0f;
    //    gameObject.transform.position = new Vector3(83f, 4f, 40f);
    //}
}
