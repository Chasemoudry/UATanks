using System.Collections;
using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(Animator), typeof(IVehicle))]
public class Vehicle_AI : MonoBehaviour, INavigator
{
	public Transform[] PatrolWaypoints { get { return this.patrolWaypoints; } }
	public float HearingDistance { get { return this.hearingDistance; } }
	public LayerMask SightLayerMask { get { return this.sightLayerMask; } }
	public int SightFOV_Total { get { return this.sightFOV_Total; } }
	public int SightFOV_Focused { get { return this.sightFOV_Focused; } }

	public Transform CurrentTarget { get; private set; }

	[Header("Navigation")]
	[SerializeField]
	private Transform[] patrolWaypoints;

	[Header("Hearing")]
	[SerializeField]
	private float hearingDistance = 5;

	[Header("Sight")]
	[SerializeField]
	private LayerMask sightLayerMask;
	[SerializeField, Range(0, 50)]
	private float sightRadius = 10;
	[SerializeField, Range(0, 180)]
	private int sightFOV_Total = 45;
	[SerializeField, Range(0, 180)]
	private int sightFOV_Focused = 30;

	private Animator animator { get; set; }

	private Vector3 testOffset = new Vector3(0, 0.5f, 0);

	private void Awake()
	{
		if (this.sightFOV_Focused > this.sightFOV_Total)
		{
			this.sightFOV_Focused = this.sightFOV_Total;
		}

		this.animator = this.GetComponent<Animator>();
	}

	private void OnEnable()
	{
		if (this.sightFOV_Total > 0)
		{
			this.StartCoroutine("CastEyesight");
		}
	}

	private void OnDisable()
	{
		this.StopAllCoroutines();
	}

	private IEnumerator CastEyesight()
	{
		this.CurrentTarget = GameManager.PlayerOne.transform;

		while (true)
		{
			// OPTION: Change to SphereCast
			Collider[] colliders = Physics.OverlapSphere(this.transform.position, this.sightRadius, this.sightLayerMask);
			bool foundTarget = false;

			foreach (Collider potentialTarget in colliders)
			{
				if (potentialTarget.tag == "Player")
				{
					var ray = new Ray(this.transform.position + this.testOffset,
						(potentialTarget.transform.position + this.testOffset) - this.transform.position + this.testOffset);
					Debug.DrawRay(ray.origin, ray.direction, Color.green);

					RaycastHit hitInfo;

					if (Physics.Raycast(ray, out hitInfo, this.sightRadius, this.sightLayerMask))
					{
						if (hitInfo.collider == potentialTarget)
						{
							this.CurrentTarget = potentialTarget.transform;
							foundTarget = true;
							break;
						}
					}
				}
			}

			if (foundTarget)
			{
				float transformAngle = (Vector3.Angle(this.transform.forward, this.CurrentTarget.position - this.transform.position));

				this.animator.SetBool("Target_InSight", transformAngle <= this.sightFOV_Total);
				this.animator.SetBool("Target_InFocus", transformAngle <= this.sightFOV_Focused);
			}
			else
			{
				this.animator.SetBool("Target_InSight", false);
				this.animator.SetBool("Target_InFocus", false);
			}

			yield return null;
		}
	}
}
