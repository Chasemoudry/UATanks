using System.Collections;
using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(Animator))]
public class Vehicle_AI_Sensor : MonoBehaviour, ISensor
{
	public Transform CurrentTarget { get; private set; }
	public Vector3 LastPOI { get; private set; }

	[Header("Sight")]
	[SerializeField]
	private LayerMask sightLayerMask;
	[SerializeField, Range(0, 60)]
	private float sightRadius = 30;
	[SerializeField, Range(0, 180)]
	private int sightFOV_Total = 45;
	[SerializeField, Range(0, 180)]
	private int sightFOV_Focused = 15;

	[Header("Hearing")]
	[SerializeField]
	private float hearingDistance = 5;

	private Animator animator;
	private Collider[] colliders = new Collider[15];

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
			this.StartCoroutine("Sense");
		}
	}

	private void OnDisable()
	{
		this.StopAllCoroutines();

#if UNITY_EDITOR == false
		this.animator.SetBool("Target_IsAudible", false);
		this.animator.SetBool("Target_InSight", false);
		this.animator.SetBool("Target_InFocus", false);
#endif
	}

	public IEnumerator Sense()
	{
		while (true)
		{
			if (this.CurrentTarget != null)
			{
				if (Vector3.Distance(this.transform.position, this.CurrentTarget.position) <= this.hearingDistance)
				{
					this.animator.SetBool("Target_IsAudible", true);
					this.UpdatePOI();
				}
				else
				{
					this.animator.SetBool("Target_IsAudible", false);
				}
			}
			else
			{
				this.animator.SetBool("Target_IsAudible", false);
			}

			if (CanSeeTarget(this.CurrentTarget) == false)
			{
				this.CurrentTarget = null;

				// TODO: Automatic array sizing
				Physics.OverlapSphereNonAlloc(this.transform.position, this.hearingDistance, this.colliders, this.sightLayerMask);

				foreach (Collider potentialTarget in this.colliders)
				{
					if (potentialTarget == null)
					{
						break;
					}

					if (potentialTarget.tag == "Player")
					{
						if (CanSeeTarget(potentialTarget.transform))
						{
							this.CurrentTarget = potentialTarget.transform;
							break;
						}
					}
				}
			}

			yield return null;
		}
	}

	public bool CanSeeTarget(Transform targetTransform)
	{
		if (targetTransform == null)
		{
			return false;
		}

		var ray = new Ray(this.transform.position + this.testOffset,
						(targetTransform.position + this.testOffset) - (this.transform.position + this.testOffset));

		// C#7: Inline out variable
		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo, this.sightRadius, this.sightLayerMask))
		{
			if (hitInfo.collider.gameObject == targetTransform.gameObject)
			{
				float transformAngle = (Vector3.Angle(this.transform.forward, targetTransform.position - this.transform.position));

#if DEBUG
				if (transformAngle <= this.sightFOV_Focused)
				{
					Debug.DrawRay(ray.origin, ray.direction * this.sightRadius, Color.cyan);
				}
				else if (transformAngle <= this.sightFOV_Total)
				{
					Debug.DrawRay(ray.origin, ray.direction * this.sightRadius, Color.green);
				}
				else
				{
					Debug.DrawRay(ray.origin, ray.direction * this.sightRadius, Color.red);
				}
#endif

				if (transformAngle <= this.sightFOV_Total)
				{
					this.UpdatePOI();
				}

				this.animator.SetBool("Target_InSight", transformAngle <= this.sightFOV_Total);
				this.animator.SetBool("Target_InFocus", transformAngle <= this.sightFOV_Focused);

				return true;
			}
		}

		this.animator.SetBool("Target_InSight", false);
		this.animator.SetBool("Target_InFocus", false);

		return false;
	}

	public void UpdatePOI()
	{
		if (this.CurrentTarget != null)
		{
			this.LastPOI = this.CurrentTarget.position;
		}
	}
}
