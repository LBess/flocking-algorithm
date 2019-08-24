using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Predator")]
public class PredatorBehavior : FilteredFlockBehavior
{
    // This is the "eating" behavior. If a predator gets close enough to an agent of a different flock, it eats them
    public float eatRadius = 0.5f;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        // If no neighbors, return 0. Edge-case for helping performance
        if (filteredContext.Count == 0)
            return Vector2.zero;

        List<FlockAgent> eatenAgents = new List<FlockAgent>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, eatRadius);
        foreach (Transform item in filteredContext)
        {
            foreach (Collider2D collider in contextColliders)
            {
                if (item.name == collider.name && item.GetComponent<FlockAgent>() != null)
                {
                    eatenAgents.Add(item.GetComponent<FlockAgent>());
                    break;
                }
                else if (item.GetComponent<FlockAgent>() == null)
                    Debug.Log("Null Flock Agent component, PredatorBehavior.cs");
            }
        }

        foreach (FlockAgent item in eatenAgents)
        {
            Flock agentFlock = item.GetComponentInParent<Flock>();
            agentFlock.RemoveAgent(item);
        }

        return Vector2.zero;
    }
}
