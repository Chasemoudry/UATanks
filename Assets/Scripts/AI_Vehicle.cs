using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class AI_Vehicle : MonoBehaviour, IVehicle
{
	// IVehicle: Event is triggered when action condition is met
	public event System.Action Action_Primary;
	// IVehicle: Event is triggered when action condition is met
	public event System.Action Action_Secondary;
	// IVehicle: Event is triggered when action condition is met
	public event System.Action Action_Death;

	// IVehicle: Property for local variable VehicleData vehicleData
	public VehicleData Data { get { return this.vehicleData; } }
	// IVehicle: Property for local variable int currentHealth
	public int Health { get { return this.currentHealth; } }

	[Header("Movement Data")]
	[SerializeField, Tooltip("Reference to VehicleData Asset which provides vehicle information.")]
	// Local reference to this vehicle's information asset.
	private VehicleData vehicleData;

	// Used to track the current health of the vehicle.
	private int currentHealth;

	private void Start()
	{
		// Set current health to max health
		this.currentHealth = this.Data.MaxHealth;

		// Add default death action to death event
		this.Action_Death += () =>
			{
				// Increment player score
				GameManager.Instance.IncrementPlayerScore(this.vehicleData.VehicleWorth);
				// Destroy this game object
				Destroy(this.gameObject);
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
			// if: Primary action event will call something
			if (this.Action_Primary != null)
			{
				// Run primary action event
				this.Action_Primary();
			}

			// if: Secondary action event will call something
			if (this.Action_Secondary != null)
			{
				// Run secondary action event
				// TODO: Implement secondary action
			}

			// Proceed to next frame
			yield return null;
		}
	}

	/// <summary>
	/// IVehicle: Damages the vehicle based on given parameters.
	/// </summary>
	/// <param name="amount">Amount of damage being taken.</param>
	public void TakeDamage(int amount)
	{
		// Decrements health
		this.currentHealth -= amount;

#if UNITY_EDITOR
		Debug.Log(this.name + "'s Health is now = " + this.currentHealth, this);
#endif

		// if: Current health is below threshhold
		if (this.currentHealth <= 0)
		{
			// Call death event
			this.Action_Death();
		}
	}
}
