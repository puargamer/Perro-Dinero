using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class log : MonoBehaviour
{
    private NavMeshAgent logNVM;
    // Start is called before the first frame update
    void Start()
    {
        logNVM = GetComponent<NavMeshAgent>();
        StartCoroutine(LogWalk());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Golem")
        {
            collision.rigidbody.AddForce(new Vector3(Random.Range(-5f, 5f), Random.Range(10f, 15f), Random.Range(-5f, 5f)), ForceMode.Impulse);
        }
    }

    IEnumerator LogWalk()
    {
        while (true)
        {
            logNVM.SetDestination(new Vector3(Random.Range(-83, -70), 0.3f, Random.Range(-17, 29)));
            yield return new WaitForSeconds(Random.Range(5, 7));
        }
    }
}
