/// <summary>
/// Interface used to track vehicle data and events.
/// </summary>
public interface IVehicle
{
	event System.Action Action_Primary;
	event System.Action Action_Secondary;
	event System.Action OnDeath;
	event System.Action OnHealthChanged;

	Vehicle_Data Data { get; }
	int CurrentHealth { get; }

	void Move(float movementAxis);
	void Rotate(float rotationAxis);
	void Raise_Action_Primary();
	void Raise_Action_Secondary();
	void TakeDamage(int amount);
	void HealDamage(int amount);
	void ModifyMovespeed(int percentageAmount, float duration);
}
