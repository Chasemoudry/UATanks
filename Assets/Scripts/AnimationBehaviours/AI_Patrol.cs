using UnityEngine;

namespace CustomBehaviours
{
	/// <summary>
	/// Custom StateMachineBehaviour which handles waypoint navigation for the animator
	/// object's INavigator component.
	/// </summary>
	public class AI_Patrol : StateMachineBehaviour
	{
		/// <summary>Animator object's INavigator component.</summary>
		private INavigator Navigator;

		/// <summary>Tracks the index of the most recently navigated waypoint.</summary>
		private int targetWaypoint;

		public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			this.Navigator = animator.GetComponent<INavigator>();

			// Set navigation speed to default value
			this.Navigator.NavAgent.speed = animator.GetComponent<IVehicle>().Data.ForwardSpeed;
			// Set the stop distance to 0 so the patrol waypoints are fully approachable
			this.Navigator.NavAgent.stoppingDistance = 0;

			// Get index of closest waypoint
			this.targetWaypoint = this.Navigator.GetClosestWaypoint();
			// Update destination to closest waypoint
			this.UpdateNavigationTarget(this.targetWaypoint);
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			// if: There is more than one waypoint
			if (this.Navigator.WaypointList.Length > 1)
			{
				// if: The current target waypoint has been reached, update current waypoint to next waypoint
				if (Vector3.Distance(animator.transform.position, this.Navigator.WaypointList[this.targetWaypoint].position) < 1)
				{
					this.UpdateNavigationTarget(this.Navigator.GetNextWaypoint(this.targetWaypoint));
				}
			}
		}

		/// <summary>
		/// Updates current navigation target to target waypoint by its index.
		/// </summary>
		/// <param name="targetWaypointIndex">The index of the target waypoint.</param>
		private void UpdateNavigationTarget(int targetWaypointIndex)
		{
			this.targetWaypoint = targetWaypointIndex;
			this.Navigator.NavAgent.SetDestination(this.Navigator.WaypointList[this.targetWaypoint].position);
		}
	}
}