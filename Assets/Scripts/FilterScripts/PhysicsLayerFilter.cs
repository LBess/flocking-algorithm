using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/PhysicsLayer")]
public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask mask;

    public override List<Transform> Filter(FlockAgent agent, List<Transform> originalNeighbors)
    {
        List<Transform> filteredNeighbors = new List<Transform>();
        foreach(Transform neighbor in originalNeighbors)
        {
            if (mask == (mask | (1 << neighbor.gameObject.layer))) {    // Some left logic bit shifting, haha
                // If the agent on the same physics layer, then add
                filteredNeighbors.Add(neighbor);
            }
        }
        
        return filteredNeighbors;
    }
}
