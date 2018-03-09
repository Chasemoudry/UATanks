using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Comments
public class AI_Data : MonoBehaviour
{
	/// <summary>
	/// 
	/// </summary>
	public float HearingDistance { get { return this.hearingDistance; } }
	/// <summary>
	/// 
	/// </summary>
	public int SightFOV_Total { get { return this.sightFOV_Total; } }
	/// <summary>
	/// 
	/// </summary>
	public int SightFOV_Focused { get { return this.sightFOV_Focused; } }
	/// <summary>
	/// 
	/// </summary>
	public List<Transform> PatrolWaypoints { get { return this.patrolWaypoints; } }

	/// <summary>
	/// 
	/// </summary>
	[SerializeField]
	private float hearingDistance;
	/// <summary>
	/// 
	/// </summary>
	[SerializeField, Range(0, 360)]
	private int sightFOV_Total;
	/// <summary>
	/// 
	/// </summary>
	[SerializeField, Range(0, 360)]
	private int sightFOV_Focused;
	/// <summary>
	/// 
	/// </summary>
	[SerializeField]
	private List<Transform> patrolWaypoints;
}
