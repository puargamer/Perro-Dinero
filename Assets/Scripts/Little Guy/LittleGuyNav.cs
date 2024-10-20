using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGuyNav : ColorUtility
{
    [Header("Distance")]
    public float followDistance = 3f;
    public float fleeDistance = 8f;

    private MaterialType materialType;
    private MaterialType wantedGiftType;
    private Transform player;
    private enum MovementState { Following, Fleeing, Still }
    private enum DesireState { None, Unwilling, Willing } // willing or unwilling to do work
    [Header("States")]
    [SerializeField] private MovementState currentMovementState;
    [SerializeField] private DesireState currentDesireState;

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Renderer materialRenderer;

    [SerializeField] private float unwillingChance = 0.1f;
    private bool hasFished = false;

    // Start is called before the first frame update
    void Start()
    {
        materialRenderer = GetComponent<Renderer>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>(); // wtf? it forced me to update the line to this
        //navMeshAgent.speed = 3.5f; // force to 3.5 prob dont need ig if i just put in inspector
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentMovementState = MovementState.Fleeing;

        currentDesireState = DesireState.None;
    }

    public void Setup(MaterialType type)
    {
        materialType = type;
        SetColor(materialRenderer, materialType);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        MovementStateAction(distanceToPlayer);

        if (hasFished) // if it has done work before
        {
            // check the of desire and if applicable roll the chance to be unwilling after the set amount of time
            CheckDesireState();
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
                    navMeshAgent.speed = 5f;
                    currentMovementState = MovementState.Following;
                    FollowPlayer(dist);
                    // add a tamed notification or something to tell the player
                    // that it is now following you
                }
                break;

            case MovementState.Still:
                // if in pen/after first fish, chance to be unwilling and want a material from the world
                // after it gets material, update desire state to willing to work again

                // condiition
                
                break;
        }
    }

    private void CheckDesireState() // somehow run this every X
    {
        if (currentDesireState == DesireState.Willing && Random.value < unwillingChance)
        {
            currentDesireState = DesireState.Unwilling;

            wantedGiftType = (MaterialType)Random.Range(0, 3);
            Debug.Log("mr cuh is now unwilling and wants a gift of " + wantedGiftType);
        }
    }

    public void ReceiveGift()
    {
        currentDesireState = DesireState.Willing;
        hasFished = false;
        Debug.Log("mr cuh got the gift and is now willing");
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

    public bool isWilling()
    {
        return currentDesireState == DesireState.Willing;
    }

    public void CaughtFish()
    {
        this.hasFished = true;
    }

    /// every X ticks, after a pikmin has caught a fish for the first time chance to roll for a successful something
    /// if it succeeds then the player needs to get something for it to get back to a willing state

}
