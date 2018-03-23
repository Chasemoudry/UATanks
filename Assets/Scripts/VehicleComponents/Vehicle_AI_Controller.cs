using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent, RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
public class Vehicle_AI_Controller : MonoBehaviour, INavigator
{
	public Transform CurrentTarget { get; set; }
	public Vector3 LastPOI { get; private set; }

	// TODO: Move INavigator to separate component
	public NavMeshAgent NavAgent { get; private set; }
	public Transform[] PatrolWaypoints { get; private set; }

	private void Start()
	{
		this.NavAgent = this.GetComponent<NavMeshAgent>();

		IVehicle vehicleComponent = this.GetComponent<IVehicle>();

		if (vehicleComponent != null)
		{
			this.NavAgent.speed = vehicleComponent.Data.ForwardSpeed;
			this.NavAgent.angularSpeed = vehicleComponent.Data.RotateSpeed;
		}
	}

	public void SetWaypoints(Transform[] waypoints)
	{
		this.PatrolWaypoints = waypoints;
	}

	public int GetClosestWaypoint()
	{
		float smallestDistance = Vector3.Distance(this.transform.position, this.PatrolWaypoints[0].position);
		int indexOfClosest = 0;

		for (int i = 0; i < this.PatrolWaypoints.Length; i++)
		{
			if (Vector3.Distance(this.transform.position, this.PatrolWaypoints[i].position) < smallestDistance)
			{
				indexOfClosest = i;
			}
		}

		return indexOfClosest;
	}

	public int GetNextWaypoint(int currentIndex)
	{
		if (this.PatrolWaypoints.Length == 0)
		{
			Debug.LogWarning("AI Controller has no waypoints, defaulting return value to 0.");
			return 0;
		}

		if (currentIndex == this.PatrolWaypoints.Length - 1)
		{
			return 0;
		}

		return currentIndex + 1;
	}

	public void UpdatePOI()
	{
		if (this.CurrentTarget != null)
		{
			this.LastPOI = this.CurrentTarget.position;
		}
	}
}
