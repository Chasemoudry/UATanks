using System.Collections;
using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(IVehicle))]
public class Vehicle_Controller : MonoBehaviour
{
	private enum ControllerType : byte
	{
		Player,
		AI
	}

	[SerializeField]
	private ControllerType controllerType = ControllerType.Player;

	private IVehicle vehicleHandler;

	private void Awake()
	{
		this.vehicleHandler = this.GetComponent<IVehicle>();
	}

	private void Start()
	{
		switch (this.controllerType)
		{
			case ControllerType.Player:
				this.vehicleHandler.OnDeath +=
					() =>
					{
						// Stop processing player input
						this.StopAllCoroutines();
						// Despawn this vehicle
						GameManager.DespawnPlayerVehicle(this.gameObject);
					};
				break;
			case ControllerType.AI:
				this.vehicleHandler.OnDeath +=
					() =>
					{
						// Increment player score
						GameManager.IncrementPlayerScore(this.GetComponent<IVehicle>().Data.VehicleWorth);
						// Despawn this vehicle
						GameManager.DespawnAIVehicle(this.gameObject);
					};
				break;
			default:
				break;
		}
	}

	private void OnEnable()
	{
		if (this.controllerType == ControllerType.Player)
		{
			// Start input enumerator
			this.StartCoroutine("ProcessInputs");
		}
	}

	private void OnDisable()
	{
		if (this.controllerType == ControllerType.Player)
		{
			// Stop input enumerator
			this.StopCoroutine("ProcessInputs");
		}
	}

	private IEnumerator ProcessInputs()
	{
		while (true)
		{
			// Move forward/back based on Vertical Input Axis
			this.vehicleHandler.Move(Input.GetAxis("Vertical"));
			// Turn left/right based on Horizontal Input Axis
			this.vehicleHandler.Rotate(Input.GetAxis("Horizontal"));

			if (Input.GetButtonDown("Primary Action") || Input.GetButton("Primary Action"))
			{
				this.vehicleHandler.Raise_Action_Primary();
			}
			else if (Input.GetButtonDown("Secondary Action"))
			{
				this.vehicleHandler.Raise_Action_Secondary();
			}

			yield return null;
		}
	}
}
