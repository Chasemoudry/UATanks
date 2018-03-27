public class Pickup_Speed : Pickup
{
	[UnityEngine.Header("Effect Settings")]
	[UnityEngine.SerializeField, UnityEngine.Range(1, 100)]
	private ushort speedPercentage = 20;
	[UnityEngine.SerializeField]
	private float effectDuration = 3f;

	protected override void Start()
	{
		this.OnActivation += this.SpeedBoost;
		base.Start();
	}

	private void SpeedBoost(IVehicle other)
	{
		other.ModifyMovespeed(this.speedPercentage, this.effectDuration);
	}
}
