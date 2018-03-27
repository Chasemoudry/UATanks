public class Pickup_Health : Pickup
{
	[UnityEngine.Header("Effect Settings")]
	[UnityEngine.SerializeField]
	private ushort healAmount = 10;

	protected override void Start()
	{
		this.OnActivation += this.HealObject;
		base.Start();
	}

	private void HealObject(IVehicle other)
	{
		other.HealDamage(this.healAmount);
	}
}
