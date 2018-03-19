using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(Collider))]
public class Projectile_Collider : MonoBehaviour
{
	[HideInInspector]
	public int damage;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag(this.tag) == false)
		{
			IVehicle vehicleData = collision.gameObject.GetComponent<IVehicle>();

			if (vehicleData != null)
			{
				vehicleData.TakeDamage(this.damage);
			}
		}

		Destroy(this.gameObject);
	}
}
