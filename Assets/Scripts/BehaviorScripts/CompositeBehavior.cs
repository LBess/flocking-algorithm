using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    // Combining multiple behaviors. Just an encapsulator for the core behaviors
    public FlockBehavior[] behaviors;
    public float[] weights; // Correlate one-one w/ the behaviors
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // Error handling, data mismatch
        if (weights.Length != behaviors.Length) 
        {
            Debug.Log("Error: Data mismatch in " + name,  this);
            return Vector2.zero;
        }

        // Set up move
        Vector2 move = Vector2.zero;

        // Iterate through each behavior
        for (int i = 0; i < behaviors.Length; ++i) 
        {
            Vector2 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];
            if (partialMove != Vector2.zero) 
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                move += partialMove;
            }
        }

        return move;
    }
}
