using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    // This behavior compels each agent to avoid collision with other agents around it.
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        // If no neighbors, return 0. Edge-case for helping performance
        if (filteredContext.Count == 0)
            return Vector2.zero;

        // Add all context transforms together and average them
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0; // Number of other agents inside of avoidance radius
        foreach (Transform item in filteredContext)
        {
            // If the item is inside of the agent's avoidance radius
            if (Vector2.SqrMagnitude(agent.transform.position - item.position) < flock.SquareAvoidanceRadius) {
                ++nAvoid;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);   // Calculates vector offset as well
            }
        }
        if (nAvoid > 0) {
            avoidanceMove /= nAvoid;
        }

        return avoidanceMove;
    }
}
