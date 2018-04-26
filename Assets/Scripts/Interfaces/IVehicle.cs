/// <summary>
/// Interface used to track vehicle data and events.
/// </summary>
public interface IVehicle
{
	event System.Action PrimarySkill;
	event System.Action SecondarySkill;
	event System.Action Death;
	event System.Action DurabilityChanged;

	Vehicle_Data Data { get; }
	int CurrentDurability { get; set; }

	void Move(float movementAxis);
	void Rotate(float rotationAxis);
	void OnPrimarySkill();
	void OnSecondarySkill();
	void TakeDamage(int amount);
	void HealDamage(int amount);
	void ModifyMovespeed(int percentageAmount, float duration);
}
