﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
	public FlockBehavior[] behaviors;
	public float[] weights;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
    	// deal with data mismatch
    	if (weights.Length != behaviors.Length) {
    		Debug.LogError("Data mismatch in " + name, this);
    		// means no move
    		return Vector2.zero;
    	}

    	// how to set move up
    	Vector2 move = Vector2.zero;

    	for (int i = 0; i < behaviors.Length; i++) {
    		Vector2 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];

    		// if there is some movement
    		if (partialMove != Vector2.zero) {
    			if (partialMove.sqrMagnitude > weights[i] * weights[i]) {
    				partialMove.Normalize();
    				partialMove *= weights[i];
    			}
    			move += partialMove;
    		}
    	}
    	return move;

    }
}