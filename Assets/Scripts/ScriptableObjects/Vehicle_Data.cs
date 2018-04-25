using UnityEngine;

[CreateAssetMenu(menuName = "Data Objects/Vehicle Data")]
public class Vehicle_Data : ScriptableObject
{
	public float ForwardSpeed { get { return this.forwardSpeed; } }

	public float ReverseSpeed { get { return this.reverseSpeed; } }

	public int RotateSpeed { get { return this.rotateSpeed; } }

	public int MaxHealth { get { return this.maxHealth; } }

	public int VehicleWorth { get { return this.vehicleWorth; } }

	[Header("Movement Settings")]
	[SerializeField, Range(1, 10)]
	private float forwardSpeed = 6;
	[SerializeField, Range(1, 10)]
	private float reverseSpeed = 3;
	[SerializeField, Range(2, 180)]
	private int rotateSpeed = 60;

	[Header("Durability Settings")]
	[SerializeField, Range(10, 200)]
	private int maxHealth = 100;

	[Header("Score Settings")]
	[Tooltip("The amount of points this vehicle awards on destruction.")]
	[SerializeField, Range(0, 200)]
	private int vehicleWorth;
}
