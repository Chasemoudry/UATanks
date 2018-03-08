using UnityEngine;

[RequireComponent(typeof(IVehicle))]
public class Trebuchet : MonoBehaviour
{
	[Header("Projectile Data")]
	[SerializeField]
	private ProjectileData projectileData;

	[Header("Launch Variables")]
	[SerializeField]
	private Transform launchPosition;
	[SerializeField]
	private float launchAngle = 45f;

	private Animator animator;
	private CharacterController charControl;
	private float nextShotTime;

	private void Awake()
	{
		this.animator = this.GetComponent<Animator>();
		this.charControl = this.GetComponent<CharacterController>();
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
		if (Time.time < this.nextShotTime)
		{
			return;
		}

		if (this.animator == null)
		{
			this.FireProjectile();
		}
		else
		{
			this.animator.SetTrigger("Fire");
		}
	}

	private void FireProjectile()
	{
		float launchRadians = (this.transform.rotation.eulerAngles.x + this.launchAngle) * Mathf.Deg2Rad;

		Vector3 launchVector = new Vector3(
			Mathf.Cos(launchRadians) * this.transform.forward.x,
			Mathf.Sin(launchRadians),
			Mathf.Cos(launchRadians) * this.transform.forward.z) * this.projectileData.ProjectileForce + this.charControl.velocity;

#if false
		Debug.Log("Angle: " + this.transform.rotation.eulerAngles.x + this.launchAngle
			+ " 2D: " + (new Vector3(Mathf.Cos(launchRadians), Mathf.Sin(launchRadians), 0))
			+ " forward: " + this.transform.forward
			+ " total: " + launchVector);
#endif

		// OPTION: Instantiate from object pool
		GameObject newProjectile = Instantiate(this.projectileData.Prefab, this.launchPosition.position, Quaternion.identity);

		newProjectile.tag = this.tag;
		newProjectile.GetComponent<Rigidbody>().AddForce(launchVector, ForceMode.VelocityChange);
		newProjectile.GetComponent<Projectile_Collider>().damage = this.projectileData.ProjectileDamage;

		Destroy(newProjectile, this.projectileData.TimeoutDuration);

		this.nextShotTime = Time.time + this.projectileData.FireRate;
	}
}
