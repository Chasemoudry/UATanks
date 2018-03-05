using System.Collections;
using UnityEngine;

public class Player_Vehicle : MonoBehaviour, IVehicle
{
	public VehicleData Data { get { return this.data; } }
	public EventManager.BasicEvent Action_Primary { get; set; }
	public EventManager.BasicEvent Action_Secondary { get; set; }
	public EventManager.BasicEvent Event_OnDeath { get; set; }

	[Header("Movement Data")]
	[SerializeField]
	private VehicleData data;

	private CharacterController controller;
	private Transform tf;

	private void Awake()
	{
		this.controller = this.GetComponent<CharacterController>();
		this.tf = this.GetComponent<Transform>();

		this.Action_Primary = () => { };
		this.Action_Secondary = () => { };
		this.Event_OnDeath = () => { EventManager.OnPlayerDeath(); };
	}

	private void OnEnable()
	{
		this.StartCoroutine("ReadInput");
	}

	private void OnDisable()
	{
		this.StopCoroutine("ReadInput");
	}

	private IEnumerator ReadInput()
	{
		while (true)
		{
			this.CalculateMovement();

			if (Input.GetButtonDown("Primary Action"))
			{
				this.Action_Primary();
			}
			else if (Input.GetButtonDown("Secondary Action"))
			{
				this.Action_Secondary();
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
