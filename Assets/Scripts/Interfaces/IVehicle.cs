public interface IVehicle
{
	event System.Action Action_Primary;
	event System.Action Action_Secondary;
	event System.Action Action_Death;

	VehicleData Data { get; }
	int Health { get; }

	void TakeDamage(int amount);
}
