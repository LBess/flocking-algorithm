using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    // Iterates through Flock Agents, applying changes
    // Handles instantiation of the Flock Agents

    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(0, 500)]    // Generates a slider b/w 10 and 500 for the startingCount var
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    void Start()
    {
        // Precomputing the squares s.t. they don't need to be recomputed for every agent each iteration
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; ++i) 
        {
            FlockAgent newAgent = Instantiate(
                // Instantiate generates new gameObjects at runtime via prefabs
                agentPrefab,
                Random.insideUnitCircle * startingCount * AgentDensity, // Agent position
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), // Agent orientation
                transform   // Agent's parent (The flock transform)
                );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    void Update()
    {
        foreach(FlockAgent agent in agents) 
        {
            List<Transform> context = GetNearbyObjects(agent);

            // FOR DEMO ONLY: Changing color based on neighbor count. NOTE: Component note cached, not efficient
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed) // Max speed exceeded
                move = move.normalized * maxSpeed;

            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        // Method 1: w/ Unity's physics system
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);    // Creates an imaginary point and space and returns which colliders are inside of it
        foreach(Collider2D contextCollider in contextColliders)
        {
            if (contextCollider.name != agent.name)
                context.Add(contextCollider.transform);
        }

        // Method 2: w/o Unity's physics system
        // foreach a in agents
        // if agent.AgentCollider.IsTouching(a)
        // add a to List<Transform>

        return context;
    }

    public bool RemoveAgent(FlockAgent agent)
    {
        agent.GetComponentInChildren<SpriteRenderer>().enabled = false;
        return agents.Remove(agent);
    }

    public void AddAgent(Vector2 pos)
    {
        FlockAgent newAgent = Instantiate(
            // Instantiate generates new gameObjects at runtime via prefabs
            agentPrefab,
            new Vector3(pos.x, pos.y, 0), // Agent position
            Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), // Agent orientation
            transform   // Agent's parent (The flock transform)
        );
        newAgent.name = "Agent " + agents.Count;
        newAgent.Initialize(this);
        agents.Add(newAgent);
    }
}
