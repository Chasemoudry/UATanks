﻿using System.Collections;
using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(Collider))]
public class Vehicle_Mover : MonoBehaviour, IVehicle
{
	// IVehicle || event: triggered when action condition is met
	public event System.Action Action_Primary;
	// IVehicle || event:
	public event System.Action Action_Secondary;
	// IVehicle || event: Triggered when vehicle health reaches zero
	public event System.Action OnDeath;
	// IVehicle || event: Triggered when health property is changed
	public event System.Action OnHealthChanged;

	// IVehicle: Property for local variable VehicleData vehicleData
	public Vehicle_Data Data { get { return this.vehicleData; } }

	// TODO: Rearrange data accessors for IVehicle interface
	public float ForwardSpeed { get { return this.Data.ForwardSpeed * this.multiplier_Movespeed; } }
	public float ReverseSpeed { get { return this.Data.ReverseSpeed * this.multiplier_Movespeed; } }

	// IVehicle: Property for local variable int currentHealth
	public int CurrentHealth
	{
		get
		{
			this.OnHealthChanged();

			return this.currentHealth;
		}
	}

	[Header("Movement Data")]
	[SerializeField, Tooltip("Reference to VehicleData Asset which provides vehicle information.")]
	// Local reference to this vehicle's information asset.
	private Vehicle_Data vehicleData;

	// Used to track the current health of the vehicle.
	private int currentHealth;
	// Local reference to CharacterController component.
	private CharacterController controller;
	// Local reference to Transform component.
	private Transform tf;
	// Movespeed multiplier
	private float multiplier_Movespeed = 1f;

	private void Awake()
	{
		this.controller = this.GetComponent<CharacterController>();
		this.tf = this.GetComponent<Transform>();
	}

	private void Start()
	{
		// Set current health equal to max health
		this.currentHealth = this.Data.MaxHealth;
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

		this.controller.SimpleMove(movementAxisValue * this.tf.forward);
	}

	/// <summary>
	/// IVehicle: Turns the vehicle based on the given axis value.
	/// </summary>
	/// <param name="rotationAxisValue">Value of rotation axis.</param>
	public void Rotate(float rotationAxisValue)
	{
		this.tf.Rotate(0, rotationAxisValue * this.Data.RotateSpeed * Time.deltaTime, 0);
	}

	/// <summary>
	/// IVehicle: Raises the primary action event.
	/// </summary>
	public void Raise_Action_Primary()
	{
		// if: The event will do something
		if (this.Action_Primary != null)
		{
			// Raise the event
			this.Action_Primary();
		}
	}

	/// <summary>
	/// IVehicle: Raises the secondary action event.
	/// </summary>
	public void Raise_Action_Secondary()
	{
		// if: The event will do something
		if (this.Action_Secondary != null)
		{
			// Raise the event
			this.Action_Secondary();
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
			// if: Death event will do something
			if (this.OnDeath != null)
			{
				// Call death event
				this.OnDeath();
			}
		}
	}

	/// <summary>
	/// IVehicle: Heals the vehicle based on given parameters.
	/// </summary>
	/// <param name="amount">Amount of damage being healed.</param>
	public void HealDamage(int amount)
	{
		// Decrements health
		this.currentHealth += amount;

		// if: Current health is above max health, set current to max
		if (this.currentHealth > this.Data.MaxHealth)
		{
			this.currentHealth = this.Data.MaxHealth;
		}

#if UNITY_EDITOR
		Debug.Log(this.name + "'s Health is now = " + this.currentHealth, this);
#endif
	}

	public void ModifyMovespeed(int percentageIncrease, float effectDuration)
	{
		this.StartCoroutine(this.Effect_Movespeed(percentageIncrease, effectDuration));
	}

	private IEnumerator Effect_Movespeed(int percentageIncrease, float effectDuration)
	{
		this.multiplier_Movespeed += percentageIncrease * 0.01f;

		yield return new WaitForSeconds(effectDuration);

		this.multiplier_Movespeed -= percentageIncrease * 0.01f;
	}
}
