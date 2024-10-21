using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReferenceHolder : MonoBehaviour
{
    public GameObject littleGuy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void returnSelection()
    {
        gameObject.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<DeploymentUI>().deploySelection = littleGuy;
    }
}
