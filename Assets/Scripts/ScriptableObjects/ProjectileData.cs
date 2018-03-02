using UnityEngine;

[CreateAssetMenu(menuName = "Data Objects/Projectile Data")]
public class ProjectileData : ScriptableObject
{
    public float ProjectileForce { get { return this.projectileForce; } }

    public float ProjectileDamage { get { return this.projectileDamage; } }

    public float TimeBetweenShots { get { return this.timeBetweenShots; } }

    public float ProjectileTimeout { get { return this.projectileTimeout; } }

    [Header("Projectile Settings")]
    [SerializeField, Range(1, 20)]
    private float projectileForce = 10;
    [SerializeField, Range(0, 20)]
    private float projectileDamage = 10;
    [SerializeField, Range(0, 10)]
    private float timeBetweenShots = 1;

    [SerializeField, Range(1, 5)]
    private float projectileTimeout = 3;
}
