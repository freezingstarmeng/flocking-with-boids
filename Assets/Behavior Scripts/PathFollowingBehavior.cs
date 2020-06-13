using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Path Following")]
public class PathFollowingBehavior : FlockBehavior
{
	private NoisyFlowfield flowField;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
        GameObject flowFieldObject = GameObject.FindWithTag("flowField");
        flowField = flowFieldObject.GetComponent<NoisyFlowfield>();
        return flowField.getDirection(agent);
    }
}
