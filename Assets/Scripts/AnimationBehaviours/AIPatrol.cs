namespace AnimationBehaviours
{
	using UnityEngine;

	/// <summary>
	/// Custom StateMachineBehaviour which handles waypoint navigation for the animator object's INavigator component.
	/// </summary>
	public class AIPatrol : StateMachineBehaviour
	{
		/// <summary>Animator object's INavigator component.</summary>
		private INavigator _navigator;

		/// <summary>Tracks the index of the most recently navigated waypoint.</summary>
		private int _targetWaypoint;

		public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			this._navigator = animator.GetComponent<INavigator>();

			// Set navigation speed to default value
			this._navigator.NavAgent.speed = animator.GetComponent<IVehicle>().Data.ForwardSpeed;
			// Set the stop distance to 0 so the patrol waypoints are fully approachable
			this._navigator.NavAgent.stoppingDistance = 0;

			// Get index of closest waypoint
			this._targetWaypoint = this._navigator.GetClosestWaypoint();
			// Update destination to closest waypoint
			this.UpdateNavigationTarget(this._targetWaypoint);
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			// if: There is more than one waypoint
			if (this._navigator.WaypointList.Length > 1)
			{
				// if: The current target waypoint has been reached, update current waypoint to next waypoint
				if (Vector3.Distance(animator.transform.position, this._navigator.WaypointList[this._targetWaypoint].position) < 1)
				{
					this.UpdateNavigationTarget(this._navigator.GetNextWaypoint(this._targetWaypoint));
				}
			}
		}

		/// <summary>
		/// Updates current navigation target to target waypoint by its index.
		/// </summary>
		/// <param name="targetWaypointIndex">The index of the target waypoint.</param>
		private void UpdateNavigationTarget(int targetWaypointIndex)
		{
			this._targetWaypoint = targetWaypointIndex;
			this._navigator.NavAgent.SetDestination(this._navigator.WaypointList[this._targetWaypoint].position);
		}
	}
}