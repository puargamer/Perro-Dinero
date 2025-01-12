using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigameMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 7;
    private Vector3 move;
    public GameObject dapUI;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dapUI = GameObject.Find("DapMinigameUI");
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right;
        move = new Vector3(move.x, 0f, move.z).normalized;
        rb.velocity = new Vector3(move.x * speed, rb.velocity.y, move.z * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "startDapMinigameTrigger")
        {
            Debug.Log("entering dap up distance");
            dapUI.GetComponent<dapUpUI>().OpenStartUI();
        }
    }
}
