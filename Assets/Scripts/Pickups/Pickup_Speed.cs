using UnityEngine;

namespace Pickups
{
	public class Pickup_Speed : Pickup
	{
		[Header("Effect Settings")]
		[SerializeField, Range(1, 100)]
		private ushort speedPercentage = 20;
		[SerializeField]
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
}