using UnityEngine;

/// <summary>
/// Interface used for simulating simple sight and hearing.
/// </summary>
public interface ISensor
{
	/// <summary>The closest trackable target.</summary>
	Transform CurrentTarget { get; }
	/// <summary>Location of last point of interest.</summary>
	Vector3 LastPOI { get; }

	/// <summary>
	/// Checks if the sensor can see the target transform via raycasting.
	/// </summary>
	/// <param name="targetTransform">The target being raycast towards.</param>
	/// <returns>Returns true if the target can be seen.</returns>
	bool CanSeeTarget(Transform targetTransform);
	/// <summary>
	/// Update the last point of interest to the last position of the current target.
	/// </summary>
	void UpdatePOI();
}
