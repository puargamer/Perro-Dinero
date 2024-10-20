using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGuyNav : MonoBehaviour
{
    [Header("Distance")]
    public float followDistance = 3f;
    public float fleeDistance = 8f;
    [SerializeField]
    private CombinationType combinationType; // this is what the little guy should be
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

    // Start is called before the first frame update
    void Start()
    {
        spriteUtility = FindObjectOfType<SpriteUtility>();
        materialRenderer = GetComponent<Renderer>(); // not sure if needed still prob not
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>(); // wtf? it forced me to update the line to this
        //navMeshAgent.speed = 3.5f; // force to 3.5 prob dont need ig if i just put in inspector
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentMovementState = MovementState.Fleeing;

        currentDesireState = DesireState.None;
        StartCoroutine(CheckDesireStateRoutine());
        //Debug.Log("little guy has started: desirestate " + currentDesireState);
    }

    public void Setup(CombinationType type)
    {
        combinationType = type;
        //SetColor(materialRenderer, materialType);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = spriteUtility.GetSprite(type);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        MovementStateAction(distanceToPlayer);
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
                    navMeshAgent.speed = 5f;
                    currentMovementState = MovementState.Following;
                    FollowPlayer(dist);
                    // add a tamed notification or something to tell the player
                    // that it is now following you
                }
                break;

            case MovementState.Still:
                // if in pen/after a fish, chance to be unwilling and want a material from the world
                // after it gets material, update desire state to willing to work again

                // condiition

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
            // subtract one from inventory
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

    public void CaughtFish() // allows the little guy to start being bad
    {
        currentDesireState = DesireState.Willing;
    }

    public void PutInPen()
    {
        //currentMovementState
    }
 }