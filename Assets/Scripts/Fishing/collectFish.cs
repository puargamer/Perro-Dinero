using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class collectFish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.tag == "Golem")
        {
            if (other.GetComponent<GolemMovement>().hasFish)
            {
                Debug.Log("goonsesh complete");
                //Singleton.Instance.fishCount++;
                //Destroy(other.transform.GetChild(other.transform.childCount - 1).gameObject);
                other.transform.GetChild(other.transform.childCount - 1).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                other.transform.GetChild(other.transform.childCount - 1).parent = null;
                other.GetComponent<GolemMovement>().hasFish = false;
            }
        }
    }
}
