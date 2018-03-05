public interface IVehicle
{
	VehicleData Data { get; }
	EventManager.BasicEvent Action_Primary { get; set; }
	EventManager.BasicEvent Action_Secondary { get; set; }
	EventManager.BasicEvent Event_OnDeath { get; set; }
}
