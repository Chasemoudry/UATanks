using System.Collections;
using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(IVehicle))]
public class Vehicle_Controller_AI : MonoBehaviour, INavigator
{
	public Vector3 PointOfInterest
	{
		get { return this.lastPOI; }
		set
		{
			this.lastPOI = value;
			this.animator.SetTrigger("RaiseLevel");
		}
	}

	[SerializeField]
	private AI_Data data;

	private IVehicle vehicleHandler;
	private Animator animator;
	private Vector3 lastPOI;

	private void Awake()
	{
		this.vehicleHandler = this.GetComponent<IVehicle>();
		this.animator = this.GetComponent<Animator>();
	}

	private void Start()
	{
		// Add sequence to Death event
		this.vehicleHandler.Event_Death += () =>
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
			// TODO: Track closest waypoint
			// TODO: Set IsVisible
			// TODO: Set IsFocused

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
		this.transform.LookAt(this.PointOfInterest);
	}

	public IEnumerator LowerLevel()
	{
		yield return new WaitForSeconds(4.0f);

		this.GetComponent<Animator>().SetTrigger("LowerLevel");

		yield break;
	}
}
