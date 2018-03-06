public interface IVehicle
{
	VehicleData Data { get; }
	event System.Action Action_Primary;
	event System.Action Action_Secondary;
	event System.Action Event_Death;
}
