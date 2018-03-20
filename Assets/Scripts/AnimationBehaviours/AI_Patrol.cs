using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Patrol : CustomBehaviour
{
	private int targetWaypoint;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		base.OnStateEnter(animator, animatorStateInfo, layerIndex);

		Debug.Log("Patrolling!");

		// Set the stop distance to 0 so the patrol waypoints are fully approachable
		this.navigator.NavAgent.stoppingDistance = 0;
		// Get index of closest waypoint
		this.targetWaypoint = this.navigator.GetClosestWaypoint();
		// Update destination to closest waypoint
		this.UpdateNavigationTarget(this.targetWaypoint);
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		if (this.navigator.PatrolWaypoints.Length > 1)
		{
			if (Vector3.Distance(animator.transform.position, this.navigator.PatrolWaypoints[this.targetWaypoint].position) < 1)
			{
				this.UpdateNavigationTarget(this.navigator.GetNextWaypoint(this.targetWaypoint));
			}
		}
	}

	private void UpdateNavigationTarget(int waypointIndex)
	{
		Debug.Log("Waypoint Update!");
		this.targetWaypoint = waypointIndex;
		this.navigator.NavAgent.SetDestination(this.navigator.PatrolWaypoints[this.targetWaypoint].position);
	}
}
