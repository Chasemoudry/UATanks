using System.Collections;
using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(IVehicle))]
public class Vehicle_Controller_AI : MonoBehaviour
{
	private IVehicle vehicleHandler;

	private void Awake()
	{
		this.vehicleHandler = this.GetComponent<IVehicle>();
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
	/// Manages basic AI state machine information.
	/// </summary>
	private IEnumerator Operate()
	{
		// Always run
		while (true)
		{
			//TODO: State Machine
			this.vehicleHandler.Raise_Action_Primary();
			this.vehicleHandler.Raise_Action_Secondary();

			// Proceed to next frame
			yield return null;
		}
	}
}
