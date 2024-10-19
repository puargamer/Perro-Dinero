using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fish : MonoBehaviour
{
    private Transform fishTr;
    private NavMeshAgent fishNVM;
    IEnumerator cor;
    // Start is called before the first frame update
    void Start()
    {
        fishTr = GetComponent<Transform>();
        fishNVM = GetComponent<NavMeshAgent>();
        cor = goonWalk();
        StartCoroutine(cor);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("goon goon fruit");
        if (other.tag == "Player")
        {
            Debug.Log("sussy balls among us");
            fishNVM.enabled = false;
            StopCoroutine(cor);
            fishTr.SetParent(other.transform);
            other.GetComponent<GolemMovement>().hasFish = true;
            StartCoroutine(collectFish(fishTr.localPosition.y));
            
        }
    }

    IEnumerator goonWalk()
    {
        while (true)
        {
            fishNVM.SetDestination(new Vector3(Random.Range(-6, 6), 0.3f, Random.Range(-2, 6)));
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }

    IEnumerator collectFish(float currY)
    {
        int collectTime = 50;
        while (collectTime > 0) 
        {
            currY = Mathf.Lerp(currY, 20f, .1f);
            fishTr.localPosition = new Vector3(0, currY, 0);
            yield return new WaitForSeconds(.02f);
            collectTime--;
        }
        
    }
}
