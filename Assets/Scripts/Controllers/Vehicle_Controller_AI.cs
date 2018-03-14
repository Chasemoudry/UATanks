using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(IVehicle))]
public class Vehicle_Controller_AI : MonoBehaviour
{
	public Vector3 PointOfInterest
	{
		get { return this.lastPOI; }
	}
    
	[SerializeField]
	private Transform[] patrolWaypoints;

	[Header("Hearing")]
	[SerializeField]
	private float hearingDistance;

	[Header("Sight")]
	[SerializeField]
	private LayerMask sightLayerMask;
	[SerializeField, Range(0, 360)]
	private int sightFOV_Total;
	[SerializeField, Range(0, 360)]
	private int sightFOV_Focused;

	private IVehicle vehicleHandler;
	private Animator animator;

	private Vector3 lastPOI;
	private int closestWaypoint;
	private Transform currentTarget;

	private void Awake()
	{
		this.vehicleHandler = this.GetComponent<IVehicle>();
		this.animator = this.GetComponent<Animator>();
	}

	private void Start()
	{
		// Add sequence to Death event
		this.vehicleHandler.Event_Death +=
			() =>
			{
				// Stop taking input
				this.StopAllCoroutines();
				// Increment player score
				GameManager.IncrementPlayerScore(this.vehicleHandler.Data.VehicleWorth);
				// Despawn this vehicle
				GameManager.DespawnAIVehicle(this.gameObject);
			};
	}

	private void OnEnable()
	{
		// Start state machine enumerator
		this.StartCoroutine("Operate");
	}

	private void OnDisable()
	{
		// Stop state machine enumerator
		this.StopCoroutine("Operate");
	}

	/// <summary>
	/// Updates UAI state machine information.
	/// </summary>
	private IEnumerator Operate()
	{
		// Always run
		while (true)
		{
			// TODO: Waypoint system
			// TODO: Set IsVisible
			// TODO: Set IsFocused

			//if: current target is the player
			//else if: current target is not the player
			if (this.currentTarget == GameManager.PlayerOne.transform)
			{
				if (CanSeeTarget(GameManager.PlayerOne.transform))
				{
					this.transform.LookAt(new Vector3(
						this.currentTarget.position.x,
						this.transform.position.y,
						this.currentTarget.position.z));

					UpdateTarget(GameManager.PlayerOne.transform);
				}
				else
				{
					//assign current target to previous target
					Debug.Log(this.name + ": Returning to closest position.");
					UpdateTarget(this.GetClosestWaypoint());
					//TODO Coroutine for alert-based targeting
				}
			}
			else
			{
				if (CanSeeTarget(GameManager.PlayerOne.transform))
				{
					//assign current target to player
					Debug.Log(this.name + ": Chasing after player. (" + GameManager.PlayerOne.name + ")", this.gameObject);
					UpdateTarget(GameManager.PlayerOne.transform);
				}
				else
				{
					if (Vector3.Distance(this.transform.position, this.currentTarget.position) < 1)
					{
						UpdateTarget(this.GetNextWaypoint());
					}
				}
			}

			// Proceed to next frame
			yield return null;
		}
	}

	public void MoveTowardsPOI()
	{
		// TODO: Navigate with NavMesh
	}

	public void LookAtPOI()
	{
		// TODO: Change to IVehicle.Rotate() call
		this.transform.LookAt(this.PointOfInterest);
	}

	public IEnumerator LowerLevel()
	{
		yield return new WaitForSeconds(4.0f);

		this.GetComponent<Animator>().SetTrigger("LowerLevel");

		yield break;
	}

	private bool CanSeeTarget(Transform target)
	{
		//vector3 angle (forward, (targetTF-thisTF)) <= FOV), raycast for seeing
		if (Vector3.Angle(this.transform.forward, target.transform.position - this.transform.position) <= 60f)
		{
			Vector3 currentPosition = this.transform.position + Vector3.up;

			var ray = new Ray(currentPosition, (target.position + Vector3.up) - currentPosition);
			RaycastHit hitInfo;

			if (this.currentTarget == GameManager.PlayerOne.transform)
			{
				if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, 50f, this.sightLayerMask))
				{
					return hitInfo.collider.transform == target;
				}
			}
			else
			{
				if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, 20f, this.sightLayerMask))
				{
					return hitInfo.collider.transform == target;
				}
			}
		}
		return false;
	}

	private void UpdateTarget(Transform newTarget)
	{
		if (newTarget != this.currentTarget)
		{
			this.currentTarget = newTarget;
		}
	}

	private void UpdateClosestWaypoint()
	{
		double smallestDistance = Vector3.Distance(
			this.transform.position,
			this.patrolWaypoints[this.closestWaypoint].position);

		for (var i = 0; i < this.patrolWaypoints.Length; i++)
		{
			if (Vector3.Distance(this.transform.position, this.patrolWaypoints[i].position) < smallestDistance)
			{
				this.closestWaypoint = i;
			}
		}
	}

	private Transform GetClosestWaypoint()
	{
		UpdateClosestWaypoint();

		Debug.Log(this.name + " is finding the closest waypoint. (Point #" + this.closestWaypoint + ")");
		return this.patrolWaypoints[this.closestWaypoint];
	}

	private Transform GetNextWaypoint()
	{
		UpdateClosestWaypoint();

		if (this.closestWaypoint == this.patrolWaypoints.Length - 1)
		{
			Debug.Log(this.name + " is finding the next waypoint.(Point #0)");
			return this.patrolWaypoints[0];
		}

		Debug.Log(this.name + " is finding the next waypoint. (" + this.patrolWaypoints[this.closestWaypoint + 1].name + ")");
		return this.patrolWaypoints[this.closestWaypoint + 1];
	}
}
