using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(Animator), typeof(IVehicle))]
public class Vehicle_AI_Controller : MonoBehaviour, INavigator
{
	public Transform[] PatrolWaypoints { get { return this.patrolWaypoints; } }
	public Transform CurrentTarget { get; set; }

	[Header("Navigation")]
	[SerializeField]
	private Transform[] patrolWaypoints;
}
