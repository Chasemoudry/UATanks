using UnityEngine;

namespace Pickups
{
	public class Pickup_Health : Pickup
	{
		[Header("Effect Settings")]
		[SerializeField]
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
}
