using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialoguetrigger : MonoBehaviour
{
    public GameObject npc;
    public Dialogue asd;
    // Start is called before the first frame update
    void Start()
    {
        asd = npc.GetComponent<Dialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        asd.StartDialogue();
    }
}
