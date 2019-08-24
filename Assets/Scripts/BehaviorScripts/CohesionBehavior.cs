using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredFlockBehavior
{
    // This behavior compels each agent to stay cohesive/close to those in its flock.
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        // If no neighbors, return 0. Edge-case for helping performance
        if (filteredContext.Count == 0)
            return Vector2.zero;

        // Add all context transforms together and average them
        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform item in filteredContext)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= filteredContext.Count;

        // Now change the position of the cohesionMove vector from the origin to the agent's position
        // Offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;  // If this is +=, it makes the agents move AWAY from eachother

        return cohesionMove;
    }
}
