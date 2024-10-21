using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NachosFish : Interact
{
    public bool isHeld;
    public bool throwDelay;
    private NavMeshAgent fishNVM;
    private Collider fishCol;
    private Transform fishTr;
    public GameObject golem;
    IEnumerator cor;

    // Start is called before the first frame update
    void Start()
    {
        fishTr = GetComponent<Transform>();
        fishNVM = GetComponent<NavMeshAgent>();
        fishCol = GetComponent<Collider>();
        cor = goonWalk();
        StartCoroutine(cor);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isHeld && throwDelay == false) { isHeld = false; }
    }

    void LateUpdate()
    {
        if (isHeld) { transform.position = GameObject.Find("Player").GetComponent<PlayerMovement>().heldObjectPos.transform.position; }
    }

    public override void interact()
    {
        isHeld = true;
        throwDelay = true;
        StartCoroutine(NextLineWait());
    }

    IEnumerator NextLineWait()
    {
        yield return new WaitForSeconds(.3f);
        throwDelay = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("goon goon fruit");
        if (other.tag == "Golem" && !other.GetComponent<GolemMovement>().hasFish)
        {
            Debug.Log("sussy balls among us");
            fishCol.isTrigger = false;
            fishNVM.enabled = false;
            StopCoroutine(cor);
            fishTr.SetParent(other.transform);
            other.GetComponent<GolemMovement>().hasFish = true;
            StartCoroutine(collectFish(fishTr.localPosition.y));

        }
    }

    IEnumerator collectFish(float currY)
    {
        int collectTime = 50;
        while (collectTime > 0)
        {
            currY = Mathf.Lerp(currY, 1.2f, .1f);
            fishTr.localPosition = new Vector3(0, currY, 0);
            yield return new WaitForSeconds(.02f);
            collectTime--;
        }

    }

    IEnumerator goonWalk()
    {
        while (true)
        {
            //Debug.Log("fish is goonsesh maxxing");
            fishNVM.SetDestination(new Vector3(Random.Range(-93, -60), 0.3f, Random.Range(-27, 39)));
            yield return new WaitForSeconds(Random.Range(1, 7));
        }
    }

}
