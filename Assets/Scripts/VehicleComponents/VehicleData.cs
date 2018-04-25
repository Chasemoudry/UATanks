using System.Collections;
using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(CharacterController))]
public class VehicleData : MonoBehaviour, IVehicle
{
	// IVehicle || Event: Triggered when action condition is met
	public event System.Action PrimarySkill;
	// IVehicle || Event: Triggered when action condition is met
	public event System.Action SecondarySkill;
	// IVehicle || Event: Triggered when vehicle health reaches zero
	public event System.Action Death;
	// IVehicle || Event: Triggered when health property is changed
	public event System.Action DurabilityChanged;

	// IVehicle || Property for local variable VehicleData vehicleData
	public Vehicle_Data Data { get { return this._vehicleData; } }

	// IVehicle: Property for local variable int currentHealth
	public int CurrentDurability
	{
		get
		{
			return this._currentDurability;
		}

		set
		{
			this._currentDurability = value;

			this.OnDurabilityChanged();
		}
	}

	// TODO: Rearrange data accessors for IVehicle interface to include movement modifiers
	private float ForwardSpeed { get { return this.Data.ForwardSpeed * this._speedMultiplier; } }
	private float ReverseSpeed { get { return this.Data.ReverseSpeed * this._speedMultiplier; } }

	[Header("Movement Data")]
	[SerializeField, Tooltip("Reference to VehicleData Asset which provides vehicle information.")]
	// Local reference to this vehicle's information asset.
	private Vehicle_Data _vehicleData;

	// Local reference to CharacterController component.
	private CharacterController _characterController;
	// Used to track the current health of the vehicle.
	private int _currentDurability;
	// Movespeed multiplier
	private float _speedMultiplier = 1f;

	private void Awake()
	{
		this._characterController = this.GetComponent<CharacterController>();
	}

	private void Start()
	{
		// Set current health equal to max health
		this._currentDurability = this.Data.MaxHealth;
	}

	/// <summary>
	/// IVehicle: Moves the vehicle based on the given axis value.
	/// </summary>
	/// <param name="movementAxisValue">Value of the movement axis.</param>
	public void Move(float movementAxisValue)
	{
		if (movementAxisValue > 0)
		{
			movementAxisValue *= this.ForwardSpeed;
		}
		else
		{
			movementAxisValue *= this.ReverseSpeed;
		}

		this._characterController.SimpleMove(movementAxisValue * this.transform.forward);
	}

	/// <summary>
	/// IVehicle: Turns the vehicle based on the given axis value.
	/// </summary>
	/// <param name="rotationAxisValue">Value of rotation axis.</param>
	public void Rotate(float rotationAxisValue)
	{
		this.transform.Rotate(0, rotationAxisValue * this.Data.RotateSpeed * Time.deltaTime, 0);
	}

	/// <summary>
	/// IVehicle: Raises the primary action event.
	/// </summary>
	public void OnPrimarySkill()
	{
		System.Action handler = this.PrimarySkill;

		// if: The event will do something
		if (handler != null)
		{
			// Raise the event
			handler();
		}
	}

	/// <summary>
	/// IVehicle: Raises the secondary action event.
	/// </summary>
	public void OnSecondarySkill()
	{
		System.Action handler = this.SecondarySkill;

		// if: The event will do something
		if (handler != null)
		{
			// Raise the event
			handler();
		}
	}

	private void OnDurabilityChanged()
	{
		System.Action handler = this.DurabilityChanged;

		// if: The event will do something
		if (handler != null)
		{
			// Raise the event
			handler();
		}
	}

	private void OnDeath()
	{
		System.Action handler = this.Death;

		// if: The event will do something
		if (handler != null)
		{
			// Raise the event
			handler();
		}
	}

	/// <summary>
	/// IVehicle: Damages the vehicle based on given parameters.
	/// </summary>
	/// <param name="amount">Amount of damage being taken.</param>
	public void TakeDamage(int amount)
	{
		// Decrements health
		this._currentDurability -= amount;

#if UNITY_EDITOR
		Debug.Log(this.name + "'s Health is now = " + this._currentDurability, this);
#endif

		// if: Current health is depleted
		if (this._currentDurability <= 0)
		{
			OnDeath();
		}
	}

	/// <summary>
	/// IVehicle: Heals the vehicle based on given parameters.
	/// </summary>
	/// <param name="amount">Amount of damage being healed.</param>
	public void HealDamage(int amount)
	{
		// if: Current health is above max health, set current to max
		if (this._currentDurability + amount > this.Data.MaxHealth)
		{
			this.CurrentDurability = this.Data.MaxHealth;
		}
		// else: Add amount to current health
		else
		{
			this.CurrentDurability += amount;
		}

#if UNITY_EDITOR
		Debug.Log(this.name + "'s Health is now = " + this._currentDurability, this);
#endif
	}

	public void ModifyMovespeed(int percentageIncrease, float effectDuration)
	{
		this.StartCoroutine(this.Effect_Movespeed(percentageIncrease, effectDuration));
	}

	private IEnumerator Effect_Movespeed(int percentageIncrease, float effectDuration)
	{
		// TODO: Change to lerp
		this._speedMultiplier += percentageIncrease * 0.01f;

		yield return new WaitForSeconds(effectDuration);

		this._speedMultiplier -= percentageIncrease * 0.01f;
	}
}
