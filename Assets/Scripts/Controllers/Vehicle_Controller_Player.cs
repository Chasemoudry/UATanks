using System.Collections;
using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(IVehicle))]
public class Vehicle_Controller_Player : MonoBehaviour
{
	private IVehicle vehicleHandler;

	private void Awake()
	{
		this.vehicleHandler = this.GetComponent<IVehicle>();
	}

	private void Start()
	{
		this.vehicleHandler.Event_Death += () =>
			{
				this.StopAllCoroutines();
				GameManager.DespawnPlayerVehicle(this.gameObject);
			};
	}

	private void OnEnable()
	{
		// Start input enumerator
		this.StartCoroutine("ProcessInputs");
	}

	private void OnDisable()
	{
		// Stop input enumerator
		this.StopCoroutine("ProcessInputs");
	}

	private IEnumerator ProcessInputs()
	{
		while (true)
		{
			// Move forward/back based on Vertical Input Axis
			this.vehicleHandler.Move(Input.GetAxis("Vertical"));
			// Turn left/right based on Horizontal Input Axis
			this.vehicleHandler.Rotate(Input.GetAxis("Horizontal"));

			if (Input.GetButtonDown("Primary Action"))
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
