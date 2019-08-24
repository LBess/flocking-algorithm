using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/SameFlock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> originalNeighbors)
    {
        List<Transform> filteredNeighbors = new List<Transform>();
        foreach(Transform neighbor in originalNeighbors)
        {
            FlockAgent neighborAgent = neighbor.GetComponent<FlockAgent>();
            if (neighborAgent != null && neighborAgent.AgentFlock == agent.AgentFlock)  // If the agent is in the same flock, add it to the list
                filteredNeighbors.Add(neighbor);
        }
        
        return filteredNeighbors;
    }
}
