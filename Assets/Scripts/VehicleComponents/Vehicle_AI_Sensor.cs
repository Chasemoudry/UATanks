using System.Collections;
using UnityEngine;

public class Vehicle_AI_Sensor : MonoBehaviour, ISensor
{
	public LayerMask SightLayerMask { get { return this.sightLayerMask; } }
	public int SightFOV_Total { get { return this.sightFOV_Total; } }
	public int SightFOV_Focused { get { return this.sightFOV_Focused; } }
	public float HearingDistance { get { return this.hearingDistance; } }

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
	private INavigator navigator;

	private Vector3 testOffset = new Vector3(0, 0.5f, 0);

#if DEBUG
	private Color debugRayColor = Color.green;
#endif

	private void Awake()
	{
		if (this.sightFOV_Focused > this.sightFOV_Total)
		{
			this.sightFOV_Focused = this.sightFOV_Total;
		}

		this.animator = this.GetComponent<Animator>();
		this.navigator = this.GetComponent<INavigator>();
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

	public IEnumerator CastEyesight()
	{
		while (true)
		{
			if (CanSeeTarget(this.navigator.CurrentTarget) == false)
			{
				this.navigator.CurrentTarget = null;

				// TODO: Optimize GC
				Collider[] colliders = Physics.OverlapSphere(this.transform.position, this.sightRadius, this.sightLayerMask);

				foreach (Collider potentialTarget in colliders)
				{
					if (potentialTarget.tag == "Player")
					{
						if (CanSeeTarget(potentialTarget.transform))
						{
							this.navigator.CurrentTarget = potentialTarget.transform;
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
					Debug.DrawRay(ray.origin, ray.direction * this.sightRadius, Color.red);
				}
				else if (transformAngle <= this.sightFOV_Total)
				{
					Debug.DrawRay(ray.origin, ray.direction * this.sightRadius, Color.yellow);
				}
				else
				{
					Debug.DrawRay(ray.origin, ray.direction * this.sightRadius, Color.green);
				}
#endif

				this.animator.SetBool("Target_InSight", transformAngle <= this.sightFOV_Total);
				this.animator.SetBool("Target_InFocus", transformAngle <= this.sightFOV_Focused);

				return true;
			}
		}

		this.animator.SetBool("Target_InSight", false);
		this.animator.SetBool("Target_InFocus", false);

#if DEBUG
		Debug.DrawRay(ray.origin, ray.direction * this.sightRadius, Color.green);
#endif

		return false;
	}
}
