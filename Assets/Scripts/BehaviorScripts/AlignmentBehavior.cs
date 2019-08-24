using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{
    // This behavior compels each agent to align its heading to be similar to those in its flock.
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        // If no neighbors, return current heading. Edge-case for helping performance
        if (filteredContext.Count == 0)
            return agent.transform.up;

        // Add all context transforms together and average them
        Vector2 alignmentMove = Vector2.zero;
        foreach (Transform item in filteredContext)
        {
            alignmentMove += (Vector2)item.transform.up;
        }
        alignmentMove /= filteredContext.Count;

        // No offset needed since the alignment is independent of the agent's position

        return alignmentMove;   // Note that alignment move is normalized, so it has a magnitude of 1, so it will move somewhat
    }
}
