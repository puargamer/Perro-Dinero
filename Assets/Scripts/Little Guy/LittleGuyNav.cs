using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGuyNav : ColorUtility
{
    [Header("Distance")]
    public float followDistance = 3f;
    public float fleeDistance = 8f;

    private MaterialType materialType;
    private Transform player;
    private enum State { Following, Fleeing }
    [Header("States")]
    [SerializeField] private State currentState;

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Renderer materialRenderer;

    // Start is called before the first frame update
    void Start()
    {
        materialRenderer = GetComponent<Renderer>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>(); // wtf? it forced me to update the line to this
        //navMeshAgent.speed = 3.5f; // force to 3.5 prob dont need ig if i just put in inspector
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = State.Fleeing;
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

        switch (currentState)
        {
            case State.Following:
                FollowPlayer(distanceToPlayer);
                break;

            case State.Fleeing:
                FleeFromPlayer(distanceToPlayer);
                if (distanceToPlayer < 1.5f) // if you next to/near it
                {
                    navMeshAgent.speed = 5f;
                    currentState = State.Following;
                }
                break;
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

}
