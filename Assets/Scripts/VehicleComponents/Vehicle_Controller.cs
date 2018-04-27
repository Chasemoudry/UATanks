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
			// Vehicle is controlled by a player
			case ControllerType.Player:
				// Add a sequence to the death event
				this.vehicleHandler.Death +=
					() =>
					{
						// Stop input coroutine
						this.StopCoroutine("ProcessInputs");
						// Despawn this vehicle
						GameManager.DespawnPlayerVehicle(this.gameObject);
						GameManager.RaiseGameOver();
					};
				break;
			// Vehicle is controlled by an AI
			case ControllerType.AI:
				// Add a handler to the death event
				this.vehicleHandler.Death +=
					() =>
					{
						// Increment player score
						GameManager.IncrementPlayerScore(this.vehicleHandler.Data.VehicleWorth);
						// Despawn this vehicle
						GameManager.DespawnAIVehicle(this.gameObject);
					};
				break;
			default:
				Debug.LogError("Invalid Controller Type!");
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
				this.vehicleHandler.OnPrimarySkill();
			}

			if (Input.GetButtonDown("Secondary Action"))
			{
				this.vehicleHandler.OnSecondarySkill();
			}

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				GameManager.EndGame();
			}

			yield return null;
		}
	}
}
