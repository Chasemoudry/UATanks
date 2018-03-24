using UnityEngine;

[RequireComponent(typeof(IVehicle))]
public class Trebuchet : MonoBehaviour, IWeapon
{
	private const float attackAnimationLength = 1.5f;

	[Header("Projectile Data")]
	[SerializeField]
	private Projectile_Data projectileData;

	[Header("Launch Variables")]
	[SerializeField]
	private Transform launchPosition;
	[SerializeField]
	private float launchAngle = 45f;

	private Animator animator;
	private CharacterController controller;
	private float nextShotTime;

	private void Awake()
	{
		this.animator = this.GetComponent<Animator>();
		this.controller = this.GetComponent<CharacterController>();

		if (this.animator != null)
		{
			this.animator.SetFloat("AttackSpeed", attackAnimationLength / this.projectileData.FireRate);
		}
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
			this.Attack();
		}
		else
		{
			this.animator.SetBool("Attack", true);
		}
	}

	public void Attack()
	{
		float launchRadians = (this.transform.rotation.eulerAngles.x + this.launchAngle) * Mathf.Deg2Rad;
		Vector3 currentVelocity = Vector3.zero;

		if (this.controller != null)
		{
			currentVelocity = this.controller.velocity;
		}

		Vector3 launchVector = new Vector3(
			Mathf.Cos(launchRadians) * this.transform.forward.x,
			Mathf.Sin(launchRadians),
			Mathf.Cos(launchRadians) * this.transform.forward.z) * this.projectileData.ProjectileForce + currentVelocity;

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
