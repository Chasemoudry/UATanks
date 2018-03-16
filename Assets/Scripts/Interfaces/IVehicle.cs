using UnityEngine;

public interface IVehicle
{
	event System.Action Action_Primary;
	event System.Action Action_Secondary;
	event System.Action Event_Death;

	Vehicle_Data Data { get; }
	int Health { get; }

	void Move(float movementAxis);
	void Rotate(float rotationAxis);
	void Raise_Action_Primary();
	void Raise_Action_Secondary();
	void TakeDamage(int amount);
}
