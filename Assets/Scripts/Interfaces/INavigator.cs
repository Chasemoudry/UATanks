using UnityEngine;
using UnityEngine.AI;

public interface INavigator
{
	NavMeshAgent NavAgent { get; }
	Transform[] PatrolWaypoints { get; }
	Transform CurrentTarget { get; set; }
	Vector3 LastPOI { get; }

	int GetClosestWaypoint();
	int GetNextWaypoint(int currentIndex);
	void UpdatePOI();
}
