using System.Collections;
using UnityEngine;

public interface ISensor
{
	LayerMask SightLayerMask { get; }
	int SightFOV_Total { get; }
	int SightFOV_Focused { get; }
	float HearingDistance { get; }

	IEnumerator Sense();
	bool CanSeeTarget(Transform target);
}
