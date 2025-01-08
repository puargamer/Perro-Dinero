using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LittleGuyNav : MonoBehaviour
{
    public int currentId;
    public Transform pen; // prob can define later
    public float cooldown = 2f;
    private bool isCooldownActive = false;
    private bool isInPen = false;
    [Header("Distance")]
    public float followDistance = 3f;
    public float fleeDistance = 8f;
    [SerializeField]
    public CombinationType combinationType; // this is what the little guy should be
    private MaterialType wantedGiftType; // wanted item from spawns
    private Transform player;
    private enum MovementState { Following, Fleeing, Still }
    private enum DesireState { None, Unwilling, Willing } // willing or unwilling to do work
    [Header("States")]
    [SerializeField] private MovementState currentMovementState;
    [SerializeField] private DesireState currentDesireState;

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Renderer materialRenderer;
    private SpriteRenderer spriteRenderer;
    private SpriteUtility spriteUtility;
    [Header("Desire Settings")]
    [SerializeField] private float unwillingChance = 0.05f;
    [SerializeField] private float checkInterval = 5f;

    [Header("Animator")]
    public Animator animator;

    public bool isBeingControlled = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteUtility = FindObjectOfType<SpriteUtility>();
        materialRenderer = GetComponent<Renderer>(); // not sure if needed still prob not
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //navMeshAgent.speed = 3.5f; // force to 3.5 for fleeing state
        player = GameObject.FindGameObjectWithTag("PlayerBody").transform; // set to player
        currentMovementState = MovementState.Fleeing;

        currentDesireState = DesireState.None;
        StartCoroutine(CheckDesireStateRoutine());
        //Debug.Log("little guy has started: desirestate " + currentDesireState);

        //random animation speed
        animator.speed = Random.Range(.8f, 1.2f);
    }

    public void Setup(CombinationType type)
    {
        this.currentId = Singleton.Instance.GrabNewId(); // id the little guy
        combinationType = type;
        //SetColor(materialRenderer, materialType);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = spriteUtility.GetSprite(type);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBeingControlled) 
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            MovementStateAction(distanceToPlayer); 
        }
    }

    private void MovementStateAction(float dist)
    {
        switch (currentMovementState)
        {
            case MovementState.Following:
                FollowPlayer(dist);
                // can also not want to work in here
                break;

            case MovementState.Fleeing:
                FleeFromPlayer(dist);
                if (dist < 1.5f) // if you next to/near it
                {
                    navMeshAgent.speed = 10f;
                    navMeshAgent.acceleration = 18f;
                    currentMovementState = MovementState.Following;
                    FollowPlayer(dist);
                    // add a tamed notification or something to tell the player
                    // that it is now following you
                }
                break;

            case MovementState.Still:

                // if in pen/after a fish, chance to be unwilling and want a material from the world
                // after it gets material, update desire state to willing to work again

                // literally do nothing in this state

                break;
        }
    }

    private IEnumerator CheckDesireStateRoutine()
    {
        //Debug.Log("starting desirestateroutine");
        while (true) {
            {
                yield return new WaitForSeconds(checkInterval);
                CheckDesireState();
            }
        }
    }

    private void CheckDesireState() // somehow run this every X
    {
        //Debug.Log("checking desire state of " + currentDesireState);
        if (currentDesireState == DesireState.Willing && Random.value < unwillingChance)
        {
            currentDesireState = DesireState.Unwilling;

            wantedGiftType = (MaterialType)Random.Range(0, 3);
            Debug.Log("mr cuh is now unwilling to work and wants a gift of " + wantedGiftType);
        }
    }

    public void ReceiveGift(MaterialType gift)
    {
        if (gift == wantedGiftType)
        {
            // subtract one from inventory in the singleton
            Singleton.Instance.RemoveMat((int)wantedGiftType);
            currentDesireState = DesireState.None;
            Debug.Log("mr cuh got the gift and is now good til you catch another fish");
        } else
        {
            Debug.Log("wrong gift type! he wants a " + wantedGiftType);
        }
    }

    private void FollowPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer > followDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Vector3 targetPosition = player.position - directionToPlayer * followDistance;

            navMeshAgent.SetDestination(targetPosition);
        }
    }

    private void FleeFromPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer < fleeDistance)
        {
            Vector3 fleeDirection = (transform.position - player.position).normalized;
            Vector3 fleeTarget = transform.position + fleeDirection * fleeDistance;
            navMeshAgent.SetDestination(fleeTarget);
        }
    }

    public void SetPen(Transform pen) // manually set pen ig :sob:
    {
        this.pen = pen;
    }

    public void CaughtFish() // allows the little guy to start being bad
    {
        currentDesireState = DesireState.Willing;
    }

    public bool IsWillingToFish()
    {
        return DesireState.Willing == currentDesireState;
    }

    // going into pen logic

    public bool PutInPen()
    { // returns false if on cooldown or in pen, returns true if executed properly
        if (isCooldownActive || isInPen) return false;
        navMeshAgent.speed = 25f;
        navMeshAgent.acceleration = 35f;
        isCooldownActive = true;
        isInPen = true;

        currentMovementState = MovementState.Still;
        navMeshAgent.SetDestination(pen.position);
        StartCoroutine(StashSequence());
        StartCoroutine(CooldownRoutine());
        return true;
    }

    public bool TakeOutPen()
    { // returns false if on cooldown or not in pen, returns true if executed
        if (isCooldownActive || !isInPen) return false;
        navMeshAgent.speed = 9f;
        navMeshAgent.acceleration = 18f;
        isCooldownActive = true;
        isInPen = false;

        gameObject.SetActive(true);
        navMeshAgent.isStopped = false;

        //Vector3 outsidePenPosition = pen.position + new Vector3(1, 0, 1); // idk here yet can hard code outside the thingy
        //navMeshAgent.SetDestination(outsidePenPosition);
        StartCoroutine(SetMovementStateFollow());
        Singleton.Instance.EquipGuy(gameObject);

        StartCoroutine(CooldownRoutine());
        return true;
    }

    public IEnumerator SetMovementStateFollow()
    {
        yield return new WaitForSeconds(0.5f);
        currentMovementState = MovementState.Following;
    }

    public IEnumerator StashSequence()
    {
        yield return new WaitForSeconds(2f);
        navMeshAgent.isStopped = true;
        isInPen = true;

        transform.position = pen.position;
        gameObject.SetActive(false);
        Singleton.Instance.StashGuy(gameObject);
    }

    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldown);
        isCooldownActive = false;
    }
 }

//[System.Serializable] // save file json template
//public class LittleGuyData
//{
//    private enum MovementState { Following, Fleeing, Still }
//    private enum DesireState { None, Unwilling, Willing }

//    public int currentId;
//    public float cooldown;
//    public bool isCooldownActive;
//    public bool isInPen;
//    public float followDistance;
//    public float fleeDistance;
//    public CombinationType combinationType;
//    public MovementState currentMovementState; // unsure if needed
//    public DesireState currentDesireState; // unsure if needed

//    public LittleGuyData GetLittleGuyData()
//    {
//        LittleGuyData data = new LittleGuyData
//        {
//        };
//        return data;
//    }

//}