using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent, RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
public class Vehicle_AI_Navigator : MonoBehaviour, INavigator
{
	/// <summary><see cref="INavigator.NavAgent"/></summary>
	public NavMeshAgent NavAgent { get; private set; }

	/// <summary><see cref="INavigator.WaypointList"/></summary>
	public Transform[] WaypointList { get; private set; }

	private void Awake()
	{
		this.NavAgent = this.GetComponent<NavMeshAgent>();

		IVehicle vehicleComponent = this.GetComponent<IVehicle>();

		if (vehicleComponent != null)
		{
			this.NavAgent.speed = vehicleComponent.Data.ForwardSpeed;
			this.NavAgent.angularSpeed = vehicleComponent.Data.RotateSpeed;
		}
	}

	/// <summary>
	/// <see cref="INavigator.SetWaypoints(Transform[])"/>
	/// </summary>
	public void SetWaypoints(Transform[] waypoints)
	{
		this.WaypointList = waypoints;
	}

	/// <summary>
	/// <see cref="INavigator.GetClosestWaypoint"/>
	/// </summary>
	public int GetClosestWaypoint()
	{
		float smallestDistance = Vector3.Distance(this.transform.position, this.WaypointList[0].position);
		int indexOfClosest = 0;

		for (int i = 0; i < this.WaypointList.Length; i++)
		{
			if (Vector3.Distance(this.transform.position, this.WaypointList[i].position) < smallestDistance)
			{
				indexOfClosest = i;
			}
		}

		return indexOfClosest;
	}

	/// <summary>
	/// <see cref="INavigator.GetNextWaypoint(int)"/>
	/// </summary>
	public int GetNextWaypoint(int currentIndex)
	{
		if (this.WaypointList.Length == 0)
		{
			Debug.LogWarning("AI Controller has no waypoints, defaulting return value to 0.");
			return 0;
		}

		if (currentIndex == this.WaypointList.Length - 1)
		{
			return 0;
		}

		return currentIndex + 1;
	}
}
