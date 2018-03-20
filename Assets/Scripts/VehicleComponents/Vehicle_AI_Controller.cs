using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent, RequireComponent(typeof(Animator), typeof(NavMeshAgent), typeof(IVehicle))]
public class Vehicle_AI_Controller : MonoBehaviour, INavigator
{
	public NavMeshAgent NavAgent { get; private set; }
	public Transform[] PatrolWaypoints { get { return this.patrolWaypoints; } }
	public Transform CurrentTarget { get; set; }
	public Vector3 LastPOI { get { return this.lastPOI; } set { this.lastPOI = value; } }

	[Header("Navigation")]
	[SerializeField]
	private Transform[] patrolWaypoints = new Transform[0];
	[SerializeField]
	private Vector3 lastPOI = Vector3.zero;

	private void Awake()
	{
		this.GetComponent<Animator>().SetBool("CanPatrol", this.patrolWaypoints.Length > 0);
	}

	private void Start()
	{
		this.NavAgent = this.GetComponent<NavMeshAgent>();
		this.NavAgent.speed = this.GetComponent<IVehicle>().Data.ForwardSpeed;
		//this.NavAgent.angularSpeed = this.GetComponent<IVehicle>().Data.RotateSpeed;
	}

	public int GetClosestWaypoint()
	{
		float smallestDistance = Vector3.Distance(this.transform.position, this.patrolWaypoints[0].position);
		int indexOfClosest = 0;

		for (int i = 0; i < this.patrolWaypoints.Length; i++)
		{
			if (Vector3.Distance(this.transform.position, this.patrolWaypoints[i].position) < smallestDistance)
			{
				indexOfClosest = i;
			}
		}

		return indexOfClosest;
	}

	public int GetNextWaypoint(int currentIndex)
	{
		if (this.patrolWaypoints.Length == 0)
		{
			Debug.LogWarning("AI Controller has no waypoints, defaulting return value to 0.");
			return 0;
		}

		if (currentIndex == this.patrolWaypoints.Length - 1)
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
