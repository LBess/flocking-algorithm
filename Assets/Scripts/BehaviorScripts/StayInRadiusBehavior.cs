using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/StayInRadiusBehavior")]
public class StayInRadiusBehavior : FlockBehavior
{
    // Compels the agent to stay in the radius of the bounding circle
    public Vector2 center;
    public float radius = 15f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius;
        
        if (t < 0.9) // Ignore
            return Vector2.zero;
        else    // Move towards center
            return centerOffset * t * t;
    }
}
