﻿using UnityEngine;

[DisallowMultipleComponent]
public class Trebuchet : MonoBehaviour, IProjectileWeapon
{
	[SerializeField]
	private ProjectileData data;
	[SerializeField]
	private Transform launchPosition;
	[SerializeField]
	private float launchAngle = 45f;

	private Animator animator;
	private new Rigidbody rigidbody;

	private void Awake()
	{
		this.animator = this.GetComponent<Animator>();
		this.rigidbody = this.GetComponent<Rigidbody>();
	}

	private void OnEnable()
	{
		this.GetComponent<IVehicle>().Action_Primary += this.TriggerAnimation;
	}

	private void OnDisable()
	{
		this.GetComponent<IVehicle>().Action_Primary -= this.TriggerAnimation;
	}

	public void TriggerAnimation()
	{
		Debug.Log("Projectile Fired!");

		if (this.animator == null)
		{
			this.FireProjectile();
		}
		else
		{
			this.animator.SetTrigger("Fire");
		}
	}

	public void FireProjectile()
	{
		float launchRadians = (this.transform.rotation.eulerAngles.x + this.launchAngle) * Mathf.Deg2Rad;

		Vector3 launchVector = new Vector3(
			Mathf.Cos(launchRadians) * this.transform.forward.x,
			Mathf.Sin(launchRadians),
			Mathf.Cos(launchRadians) * this.transform.forward.z) * this.data.ProjectileForce + this.rigidbody.velocity;

		Debug.Log(this.transform.rotation.eulerAngles.x + this.launchAngle
			+ " 2d: " + (new Vector3(Mathf.Cos(launchRadians), Mathf.Sin(launchRadians), 0))
			+ " forward: " + this.transform.forward
			+ " total: " + launchVector);

		// OPTION: Instantiate from object pool
		Instantiate(this.data.Prefab, this.launchPosition.position, Quaternion.identity)
			.GetComponent<Rigidbody>()
			.AddForce(launchVector, ForceMode.VelocityChange);
	}
}
