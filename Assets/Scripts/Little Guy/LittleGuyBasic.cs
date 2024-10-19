using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGuyBasic : MonoBehaviour
{
    private Renderer materialRenderer;

    public float followDistance = 3f;
    public float fleeDistance = 6f;
    public float speed = 2f;
    private Transform player;
    private enum State { Following, Fleeing }
    private State currentState;

    // Start is called before the first frame update
    void Start()
    {
        materialRenderer = GetComponent<Renderer>();
        //SetColor(); // this should be in a setup and done later
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = State.Fleeing;
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
                // caught, follow player
                if (distanceToPlayer < 1f)
                { 
                    currentState = State.Following;
                    speed = 5f;
                }
                break;
        }
    }

    private void FollowPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer > followDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void FleeFromPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer < fleeDistance)
        {
            Vector3 direction = (transform.position - player.position).normalized; // away from player
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
