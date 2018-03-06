using System;
using System.Collections;
using UnityEngine;

public class Player_Vehicle : MonoBehaviour, IVehicle
{
	public VehicleData Data { get { return this.data; } }

	public event Action Action_Primary;
	public event Action Action_Secondary;
	public event Action Event_Death;

	[Header("Movement Data")]
	[SerializeField]
	private VehicleData data;

	private CharacterController controller;
	private Transform tf;

	private void Awake()
	{
		this.controller = this.GetComponent<CharacterController>();
		this.tf = this.GetComponent<Transform>();
	}

	private void OnEnable()
	{
		this.StartCoroutine("MovementInput");
	}

	private void OnDisable()
	{
		this.StopCoroutine("MovementInput");
	}

	private void OnPlayerDeath()
	{
		this.StopCoroutine("MovementInput");
	}

	private IEnumerator MovementInput()
	{
		while (true)
		{
			this.CalculateMovement();

			if (Input.GetButtonDown("Primary Action"))
			{
				if (this.Action_Primary != null)
				{
					this.Action_Primary();
				}
			}
			else if (Input.GetButtonDown("Secondary Action"))
			{
				if (this.Action_Secondary != null)
				{
					this.Action_Secondary();
				}
			}

			yield return null;
		}
	}

	private void CalculateMovement()
	{
		float horzAxis = Input.GetAxis("Horizontal");
		float vertAxis = Input.GetAxis("Vertical");

		if (vertAxis > 0)
		{
			vertAxis *= this.Data.ForwardSpeed;
		}
		else
		{
			vertAxis *= this.Data.ReverseSpeed;
		}

		this.tf.Rotate(0, horzAxis * this.Data.RotateSpeed * Time.deltaTime, 0);
		this.controller.SimpleMove(vertAxis * this.tf.forward);
	}
}
