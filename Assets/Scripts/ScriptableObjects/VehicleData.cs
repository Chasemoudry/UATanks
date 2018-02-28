using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data Objects/Vehicle Data")]
public class VehicleData : ScriptableObject
{
    public float ForwardSpeed { get { return this.forwardSpeed; } private set { this.forwardSpeed = value; } }

    public float ReverseSpeed { get { return this.reverseSpeed; } private set { this.reverseSpeed = value; } }

    public int RotateSpeed { get { return this.rotateSpeed; } private set { this.rotateSpeed = value; } }

    public float ProjectileForce { get { return this.projectileForce; } private set { this.projectileForce = value; } }

    public float ProjectileDamage { get { return this.projectileDamage; } private set { this.projectileDamage = value; } }

    public float TimeBetweenShots { get { return this.timeBetweenShots; } private set { this.timeBetweenShots = value; } }

    public int MaxHealth { get { return this.maxHealth; } set { this.maxHealth = value; } }

    public int VehicleWorth { get { return this.vehicleWorth; } set { this.vehicleWorth = value; } }

    [Header("Movement Settings")]
    [SerializeField, Range(1, 10)]
    private float forwardSpeed = 6;
    [SerializeField, Range(1, 10)]
    private float reverseSpeed = 3;
    [SerializeField, Range(2, 30)]
    private int rotateSpeed = 10;

    [Header("Projectile Settings")]
    [SerializeField, Range(1, 20)]
    private float projectileForce = 10;
    [SerializeField, Range(0, 20)]
    private float projectileDamage = 10;
    [SerializeField, Range(0, 10)]
    private float timeBetweenShots = 1;

    [Header("Durability Settings")]
    [SerializeField, Range(10, 200)]
    private int maxHealth = 100;

    [Header("Score Settings")]
    [Tooltip("The amount of points this vehicle awards on destruction.")]
    [SerializeField, Range(0, 200)]
    private int vehicleWorth = 0;
}
