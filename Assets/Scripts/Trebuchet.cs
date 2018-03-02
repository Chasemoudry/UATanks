using UnityEngine;

public class Trebuchet : MonoBehaviour, IProjectileWeapon
{
    [SerializeField]
    private ProjectileData projectileType;
    [SerializeField]
    private Transform launchPosition;

    private GameObject trebuchetProjectile;

    private void Awake()
    {
        this.trebuchetProjectile = Resources.Load<GameObject>("Projectile_Rock");
    }

    public void FireProjectile()
    {

    }
}
