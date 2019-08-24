using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FilteredFlockBehavior : FlockBehavior
{
    // Doesn't need to implement FlockBehavior's abstract functions b/c it is abstract
    public ContextFilter filter;
}
