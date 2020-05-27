using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
        // no neighbor no adjustment
        if(context.Count == 0)
            return Vector2.zero;

        // add all pts together and average
        Vector2 cohesionMove = Vector2.zero;
        List<Transform> FilteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach(Transform item in FilteredContext) {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= context.Count;

        // create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;
        return cohesionMove;
    }
}
