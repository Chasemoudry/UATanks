using UnityEngine;

[CreateAssetMenu(menuName = "Data Objects/Projectile Data")]
public class Projectile_Data : ScriptableObject
{
	public GameObject Prefab { get { return this.prefab; } }

	public float ProjectileForce { get { return this.projectileForce; } }

	public int ProjectileDamage { get { return this.projectileDamage; } }

	public float FireRate { get { return this.fireRate; } }

	public float TimeoutDuration { get { return this.timeoutDuration; } }

	[Header("Projectile Settings")]
	[SerializeField]
	private GameObject prefab;
	[SerializeField, Range(1, 20)]
	private float projectileForce = 10;
	[SerializeField, Range(0, 20)]
	private int projectileDamage = 10;
	[SerializeField, Range(0, 10)]
	private float fireRate = 1;
	[SerializeField, Range(1, 5)]
	private float timeoutDuration = 3;
}
