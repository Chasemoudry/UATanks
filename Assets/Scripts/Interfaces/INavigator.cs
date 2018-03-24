using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Interface used for NavMesh-driven navigation.
/// </summary>
public interface INavigator
{
	/// <summary>Reference to local NavMeshAgent.</summary>
	NavMeshAgent NavAgent { get; }
	/// <summary>Map-specific array of waypoints used for patrol-based navigation.</summary>
	Transform[] WaypointList { get; }

	/// <summary>
	/// Set function for WaypointList property.
	/// </summary>
	/// <param name="waypoints">The new list of navigable waypoints.</param>
	void SetWaypoints(Transform[] waypoints);
	/// <summary>
	/// Calculates the closest waypoint and returns its index value.
	/// </summary>
	/// <returns>Returns the index of the closest waypoint.</returns>
	int GetClosestWaypoint();
	/// <summary>
	/// Calculates the next waypoint by the current waypoint's index.
	/// </summary>
	/// <param name="currentIndex">The index of the current waypoint.</param>
	/// <returns>The index of the next waypoint.</returns>
	int GetNextWaypoint(int currentIndex);
}
