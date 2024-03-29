﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }
    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Move(Vector2 velocity)
    {
        // Turn agent in velocity direction
        transform.up = velocity;

        // Adjusting position of agent
        transform.position += (Vector3)velocity * Time.deltaTime;

        // Debugging
        //Debug.DrawRay(transform.position, transform.up, Color.red);
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }
}
