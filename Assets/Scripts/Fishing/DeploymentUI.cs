using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeploymentUI : MonoBehaviour
{
    public GameObject player;
    public GameObject childCanvas;
    private bool deployUIopen = false;
    public GameObject deployGrid;
    public GameObject lurePrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("q worked");
            deployUIopen = !deployUIopen;
            childCanvas.SetActive(deployUIopen);
            player.GetComponent<PlayerMovement>().enabled = !deployUIopen;
            Cursor.lockState = deployUIopen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = deployUIopen;
        }
    }

    public void AddToGrid(Sprite fishsticks)
    {
        GameObject newLure = Instantiate(lurePrefab, Vector3.zero, Quaternion.identity, deployGrid.transform);
        newLure.GetComponentInChildren<Image>().sprite = fishsticks;
    }
}
