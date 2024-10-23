using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tempStartFishing : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCam;
    public GameObject goon;
    //private bool goonState = false;
    public GameObject otherMenu;
    public GameObject tabText;
    //public GameObject fishingCam;
    //public GameObject fishingCamother;
    //public GameObject fishingCamParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Singleton.Instance.isLure)
        {
            Debug.Log("tab pressed");
            Singleton.Instance.menuInt--;
            tabText.SetActive(false);
            Singleton.Instance.isLure = false;
            player.GetComponent<PlayerMovement>().enabled = true;
            playerCam.SetActive(true);
            goon.GetComponent<GolemMovement>().hasFish = false;
            goon.GetComponent<GolemMovement>().enabled = false;
            goon.transform.position = new Vector3(goon.transform.position.x, .1f, goon.transform.position.z);
            goon.GetComponent<NavMeshAgent>().enabled = true;
            goon.GetComponent<LittleGuyNav>().enabled = true;
            goon.GetComponentInChildren<SpriteFaceCam>().enabled = true;
            otherMenu.SetActive(true);
            goon.GetComponentInChildren<Camera>().enabled = false;
            goon.GetComponentInChildren<lureCam>().enabled = false;
            //fishingCamother.SetActive(false);
            //fishingCam.transform.parent = fishingCamParent.transform;
        }
    }

    public void UpdateGoon(GameObject silly)
    {
        goon = silly;
    }
}
