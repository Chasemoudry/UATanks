using System.Collections;
using UnityEngine;

namespace Pickups
{
	[RequireComponent(typeof(MeshRenderer), typeof(Collider))]
	public abstract class Pickup : MonoBehaviour
	{
		protected delegate void TriggerEvent(IVehicle other);

		protected event TriggerEvent OnActivation;

		[Header("Pickup Settings")]
		[SerializeField]
		private float respawnTime = 5f;
		[SerializeField]
		private int rotationSpeed = 90;

		protected virtual void Start()
		{
			this.StartCoroutine("EnablePickup");
		}

		private IEnumerator EnablePickup()
		{
			this.GetComponent<MeshRenderer>().enabled = true;
			this.GetComponent<Collider>().enabled = true;

			while (true)
			{
				Spin();
				yield return null;
			}
		}

		private IEnumerator RespawnPickup()
		{
			this.GetComponent<MeshRenderer>().enabled = false;
			this.GetComponent<Collider>().enabled = false;

			yield return new WaitForSeconds(this.respawnTime);

			this.StartCoroutine("EnablePickup");
		}

		private void DisablePickup()
		{
			this.StopCoroutine("EnablePickup");
			this.StartCoroutine(RespawnPickup());
		}

		private void Spin()
		{
			this.transform.Rotate(0, this.rotationSpeed * Time.deltaTime, 0);
		}

		private void OnTriggerEnter(Collider other)
		{
			IVehicle vehicleComponent = other.GetComponent<IVehicle>();

			if (vehicleComponent != null)
			{
				this.OnActivation(vehicleComponent);
				this.DisablePickup();
			}
		}
	}
}